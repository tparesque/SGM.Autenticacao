using SGM.Autenticacao.Domain.Dto;
using SGM.Autenticacao.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace SGM.Autenticacao.Application.Templates
{
    public static class EmailTemplate
    {
        public static string ConfirmacaoCadastro(IConfiguration configuration, UsuarioDto usuarioDto)
        {
            var corpoEmail = $"<p>Olá {usuarioDto.Nome}, <br> <br> Você foi cadastrado na nossa plataforma e agora pode acessar todos os " +
                    $"serviços disponibilizados pelo governo do estado! " +
                    $"<br> " +
                    $"<label style='font-weight: bold;'>Acesse através do link: <a href='{configuration["Urls:Site"]}/Login'>Entrar</a> </label>" +
                    $"<br> <br>";
            return corpoEmail;
        }

        public static string RecuperarSenha(IConfiguration configuration, Usuario user, string token)
        {
            var corpoEmail = $"<p>Olá {user.Nome}, <br/> <br/> Você solicitou a recuperação de senha." +
                             $"<br/> </br>" +
                             $"<label style='font-weight: bold;'>Altere sua senha através do link: <a href='{configuration["Urls:Site"]}/recuperar-senha/atualizar-senha?token={token}&id={user.Id}'>Cadastrar Nova Senha</a> </label>" +
                             $"<br/> </br></br>";
                             
            return corpoEmail;
        }
    }
}
