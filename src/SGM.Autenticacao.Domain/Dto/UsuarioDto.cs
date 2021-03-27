using System.Collections.Generic;
using System.Security.Claims;

namespace SGM.Autenticacao.Domain.Dto
{
    public class UsuarioDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string Role { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public EnderecoDto Endereco { get; set; }
        public string DataCadastro { get; set; }

    }
}
