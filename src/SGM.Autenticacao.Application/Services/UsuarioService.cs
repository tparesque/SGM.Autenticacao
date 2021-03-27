using AutoMapper;
using SGM.Autenticacao.Application.Templates;
using SGM.Autenticacao.Application.Validators;
using SGM.Autenticacao.Domain.Dto;
using SGM.Autenticacao.Domain.Entities;
using SGM.Autenticacao.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SGM.Autenticacao.Application.Token;

namespace SGM.Autenticacao.Application.Services
{
    public class UsuarioService
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IEmailService _emailService;               

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioService(SignInManager<Usuario> signInManager,
                                   UserManager<Usuario> userManager,
                                   TokenConfigurations tokenConfigurations,
                                   IEmailService emailService,
                                   IConfiguration configuration,
                                   IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenConfigurations = tokenConfigurations;
            _emailService = emailService;
            _configuration = configuration;
            _mapper = mapper;
        }
       
        public async Task<ResultDto<AuthenticatedDto>> Login(AuthDto authDto)
        {
            var success = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Senha, false, false);            

            if (!success.Succeeded)                           
                return ResultDto<AuthenticatedDto>.Validation("Login ou senha inválidos!"); 
           
            var applicationUser = await GetUserByEmail(authDto.Email);
          
            var usuarioDto = new UsuarioDto
            {
                Id = applicationUser.Id,
                Nome = applicationUser.Nome,
                Email = applicationUser.Email,
                Role = applicationUser.Role,
                Claims = applicationUser.Claims
            };

            return ResultDto<AuthenticatedDto>.Success(TokenWrite.WriteToken(usuarioDto, _tokenConfigurations));
        }       
       
        public async Task<ResultDto<IEnumerable<UsuarioDto>>> ObterTodosUsuarios()
        {            
            var usuarios = await _userManager.Users.ToListAsync();           

            if (!usuarios.Any())
                return ResultDto<IEnumerable<UsuarioDto>>.Validation("Usuário não encontrado na base de dados!");

            var usuariosDto = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioDto>>(usuarios);
            return await Task.FromResult(ResultDto<IEnumerable<UsuarioDto>>.Success(usuariosDto));
        }

        public async Task<ResultDto<IEnumerable<UsuarioDto>>> ObterTodos()
        {
            var usuarios = await _userManager.Users.ToListAsync(); 
           
            var usuariosDto = new List<UsuarioDto>();

            foreach (var usuario in usuarios)
            {
                var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);                
                var roles = await _userManager.GetRolesAsync(usuario);
                var claims = await _userManager.GetClaimsAsync(usuario);

                usuarioDto.Role = roles.FirstOrDefault();
                usuarioDto.Claims = claims;

                usuariosDto.Add(usuarioDto);
            }
           
            return await Task.FromResult(ResultDto<IEnumerable<UsuarioDto>>.Success(usuariosDto));
        }      

        public async Task<ResultDto<UsuarioDto>> SalvarUsuario(UsuarioDto usuarioDto)
        {
            usuarioDto.Role = "Usuario";
            var result = await Salvar(usuarioDto);          
            return await Task.FromResult(result);
        }

        public async Task<ResultDto<UsuarioDto>> Salvar(UsuarioDto usuarioDto)
        {
            var usuarioDtoValidate = new UsuarioDtoValidate(usuarioDto);
            if (!usuarioDtoValidate.Validate())
                return ResultDto<UsuarioDto>.Validation(usuarioDtoValidate.Mensagens);

            var usuarioJaCadastrado = await _userManager.Users.FirstOrDefaultAsync(c => c.Email == usuarioDto.Email);            
                        
            if(usuarioJaCadastrado != null)
                return ResultDto<UsuarioDto>.Validation("Email já cadastrado!");
           
            var user = new Usuario(usuarioDto);          

            var result = await _userManager.CreateAsync(user, usuarioDto.Senha);
            usuarioDto.Id = user.Id;

            if (result.Succeeded && !string.IsNullOrWhiteSpace(usuarioDto.Role))
            {
                var usuarioDB = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == usuarioDto.Id);               
                await _userManager.AddToRoleAsync(usuarioDB, usuarioDto.Role);
                _emailService.Send(usuarioDto.Email, "Confirmação de cadastro", EmailTemplate.ConfirmacaoCadastro(_configuration, usuarioDto));
            }   
            else if(result.Errors.Any(x => x.Code == "PasswordTooShort"))
                return ResultDto<UsuarioDto>.Validation("senha deve ter no minimo 6 caracteres!");
            else
                return ResultDto<UsuarioDto>.Validation("Cadastro inválido!");

            return await Task.FromResult(ResultDto<UsuarioDto>.Success(usuarioDto));
        }

        public async Task<ResultDto<UsuarioDto>> Update(UsuarioDto usuarioDto)
        {
            var usuario = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == usuarioDto.Id);

            if (usuario == null)
                return ResultDto<UsuarioDto>.Validation("Usuário não encontrado na base de dados!");

            usuario.AtualizarUsuario(usuarioDto);

            await _userManager.UpdateAsync(usuario);

            return await Task.FromResult(ResultDto<UsuarioDto>.Success(usuarioDto));
        }

        public async Task<ResultDto<UsuarioDto>> GetUserById(string id)
        {
            var usuario = await _userManager.Users?
                .Include(x => x.Endereco)
                    .ThenInclude(x => x.Municipio)
                        .ThenInclude(x => x.Estado)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return ResultDto<UsuarioDto>.Validation("Usuário não encontrado na base de dados!");

            var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);

            if (usuarioDto != null)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                usuarioDto.Role = roles.FirstOrDefault();
            }                

            return await Task.FromResult(ResultDto<UsuarioDto>.Success(usuarioDto));
        }

        public async Task<ResultDto<bool>> EnviarEmailRecuperarSenha(string email)
        {
            var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Email == email);
            
            if (user == null)
                return ResultDto<bool>.Validation("Usuário não cadastrado no sistema!");           
                       
            var token = await GerarToken(user);
            _emailService.Send(user.Email, "Alteração de senha", EmailTemplate.RecuperarSenha(_configuration, user, token));
            return ResultDto<bool>.Success(true);
        }

        public async Task<ResultDto<bool>> RecuperarSenha(RecuperarSenhaDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            var base64EncodedBytes = System.Convert.FromBase64String(dto.Token);
            dto.Token = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NovaSenha);

            if (!result.Succeeded)
                return ResultDto<bool>.Validation("Erro ao alterar senha!");

            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<bool>> AtualizarSenha(UsuarioDto usuarioDto)
        {
            var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == usuarioDto.Id);
            if (user == null)
                return ResultDto<bool>.Validation("Usuário não encontrado na base de dados!");

            var result = await _userManager.ChangePasswordAsync(user, usuarioDto.Senha, usuarioDto.NovaSenha);

            return await Task.FromResult(ResultDto<bool>.Success(result.Succeeded));
        }

        public async Task<ResultDto<bool>> Delete(string usuarioId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (user == null)
                return ResultDto<bool>.Validation("Usuário não encontrado na base de dados!");
          
            await _userManager.DeleteAsync(user);

            return await Task.FromResult(ResultDto<bool>.Success(true));
        }        

        private async Task<UsuarioDto> GetUserByEmail(string email)
        {            
            var user = await _userManager?.Users?.FirstOrDefaultAsync(c => c.Email == email);
            var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(user);

            if (usuarioDto != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);
                usuarioDto.Role = roles.FirstOrDefault();
                usuarioDto.Claims = claims;       
            }                

            return await Task.FromResult(usuarioDto);           
        }             

        private async Task<string> GerarToken(Usuario user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(token);
            return await Task.FromResult(Convert.ToBase64String(plainTextBytes));
        }                

        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
