using SGM.Autenticacao.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGM.Autenticacao.Domain.Interfaces.Services
{
    public interface IMunicipioService
    {
        Task<ResultDto<IEnumerable<MunicipioDto>>> ObterTodosPorEstado(int estadoId);
    }
}
