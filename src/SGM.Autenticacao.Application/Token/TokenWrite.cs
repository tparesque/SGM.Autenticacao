using Microsoft.IdentityModel.Tokens;
using SGM.Autenticacao.Domain.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SGM.Autenticacao.Application.Token
{
    public class TokenWrite
    {
        public static AuthenticatedDto WriteToken(UsuarioDto usuarioDto, TokenConfigurations tokenConfigurations)
        {
            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);
            var key = Encoding.ASCII.GetBytes(tokenConfigurations.Secret);
            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Emissor,
                Audience = tokenConfigurations.ValidoEm,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = CreateClaimsIdentity(usuarioDto),
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            };

            var securityToken = handler.CreateToken(tokenDescriptor);

            var token = handler.WriteToken(securityToken);

            return new AuthenticatedDto()
            {
                Id = usuarioDto.Id,
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Role = usuarioDto?.Role,               
                IsAdministrador = usuarioDto.Role == "Administrador"
            };
        }

        private static ClaimsIdentity CreateClaimsIdentity(UsuarioDto usuarioDto)
        {
            return new ClaimsIdentity(new GenericIdentity(usuarioDto.Nome, "Login"), PrepareClaims(usuarioDto));
        }

        private static List<Claim> PrepareClaims(UsuarioDto usuarioDto)
        {
            var claims = new List<Claim>
            {                                
                new Claim(JwtRegisteredClaimNames.Sub, usuarioDto.Id),
                new Claim(JwtRegisteredClaimNames.Email, usuarioDto.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Role, usuarioDto.Role)
            };

            return claims;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}
