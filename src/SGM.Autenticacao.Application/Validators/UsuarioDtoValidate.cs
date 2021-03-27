using SGM.Autenticacao.Domain.Dto;
using SGM.Autenticacao.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace SGM.Autenticacao.Application.Validators
{
    public class UsuarioDtoValidate : IValidator
    {
        private readonly UsuarioDto _usuarioDto;
        public List<string> Mensagens { get; }

        public UsuarioDtoValidate(UsuarioDto usuarioDto)
        {
            _usuarioDto = usuarioDto;
            Mensagens = new List<string>();
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_usuarioDto.Nome))
                Mensagens.Add("Informe o nome!");

            if (string.IsNullOrWhiteSpace(_usuarioDto.Email))
                Mensagens.Add("Informe o e-mail!");

            if (string.IsNullOrWhiteSpace(_usuarioDto.Senha))
                Mensagens.Add("Informe a senha!");

            return !Mensagens.Any();
        }
    }
}
