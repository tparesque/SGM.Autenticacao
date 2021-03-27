namespace SGM.Autenticacao.Domain.Dto
{
    public class RecuperarSenhaDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NovaSenha { get; set; }
    }
}
