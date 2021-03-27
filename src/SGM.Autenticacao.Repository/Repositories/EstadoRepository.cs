using SGM.Autenticacao.Domain.Entities;
using SGM.Autenticacao.Domain.Interfaces.Repository;
using SGM.Autenticacao.Repository.Context;

namespace SGM.Autenticacao.Repository.Repositories
{
    public class EstadoRepository : Repository<Estado>, IEstadoRepository
    {
        public EstadoRepository(SGMDbContext context) : base(context)
        {
        }
    }
}
