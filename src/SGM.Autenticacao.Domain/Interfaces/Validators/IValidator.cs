using System.Collections.Generic;

namespace SGM.Autenticacao.Domain.Interfaces.Validators
{
    public interface IValidator
    {
        bool Validate();

        List<string> Mensagens { get; }
    }
}
