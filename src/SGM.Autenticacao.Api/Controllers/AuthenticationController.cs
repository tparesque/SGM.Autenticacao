using SGM.Autenticacao.Application.Services;
using SGM.Autenticacao.Application.Validators;
using SGM.Autenticacao.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SGM.Autenticacao.Api.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [Route("api/autenticacao")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;       

        public AuthenticationController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;            
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ResultDto<UsuarioDto>), 200)]
        public async Task<ResultDto<UsuarioDto>> Post([FromBody] UsuarioDto usuarioDto)
        {
            return await _usuarioService.SalvarUsuario(usuarioDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ResultDto<AuthenticatedDto>> Auth([FromBody] AuthDto authDto)
        {
            var authDtoValidador = new AuthDtoValidate(authDto);
            if (!authDtoValidador.Validate())
                return ResultDto<AuthenticatedDto>.Validation(authDtoValidador.Mensagens);

            return await _usuarioService.Login(authDto);           
        }            
      
        [AllowAnonymous]
        [HttpPost("recuperar-senha")]
        public async Task<ResultDto<bool>> RecuperarSenha(RecuperarSenhaDto dto)
        {
            return await _usuarioService.RecuperarSenha(dto);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("enviar-email-recuperacao-senha")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> EnviarEmailDeRecuperacaoDeSenha([FromBody] UsuarioDto usuarioDto)
        {
            return await _usuarioService.EnviarEmailRecuperarSenha(usuarioDto.Email);           
        }

        [AllowAnonymous]
        [HttpGet("logoff")]
        public async Task LogOff()
        {
            await _usuarioService.LogOff();
        }
    }
}
