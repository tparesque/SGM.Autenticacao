using SGM.Autenticacao.Domain.Entities;
using SGM.Autenticacao.Domain.Interfaces.Repository;
using SGM.Autenticacao.Repository.Context;

namespace SGM.Autenticacao.Repository.Repositories
{
    public class MunicipioRepository : Repository<Municipio>, IMunicipioRepository
    {
        public MunicipioRepository(SGMDbContext context) : base(context)
        {
        }
    }
}
