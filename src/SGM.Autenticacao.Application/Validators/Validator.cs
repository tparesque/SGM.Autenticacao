using SGM.Autenticacao.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace SGM.Autenticacao.Application.Validators
{
    public class Validator : IValidator
    {
        private readonly IList<IValidator> _validators;
        public List<string> Mensagens { get; }

        public Validator()
        {
            _validators = new List<IValidator>();
            Mensagens = new List<string>();
        }

        public void RegistrarValidator(IValidator validator)
        {
            _validators.Add(validator);
        }

        public bool Validate()
        {
            foreach (var validator in _validators)
            {
                if (!validator.Validate())
                    Mensagens.AddRange(validator.Mensagens);
            }

            return !Mensagens.Any();
        }
    }
}
