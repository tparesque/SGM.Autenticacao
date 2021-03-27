namespace SGM.Autenticacao.Domain.Dto
{
    public class AuthenticatedDto
    {
        public string Id { get; set; }     
        public string Email { get; set; }
        public string Nome { get; set; }
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public bool IsAdministrador { get; set; }
        
    }
}
