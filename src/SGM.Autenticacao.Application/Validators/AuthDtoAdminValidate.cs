using SGM.Autenticacao.Domain.Dto;
using SGM.Autenticacao.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace SGM.Autenticacao.Application.Validators
{
    public class AuthDtoAdminValidate : IValidator
    {
        private readonly AuthDto _authDto;
        public List<string> Mensagens { get; }

        public AuthDtoAdminValidate(AuthDto authDto)
        {
            _authDto = authDto;
            Mensagens = new List<string>();
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_authDto.Email))
                Mensagens.Add("Informe o e-mail!");

            if (string.IsNullOrWhiteSpace(_authDto.Senha))
                Mensagens.Add("Informe a senha!");           

            return !Mensagens.Any();
        }
    }
}
