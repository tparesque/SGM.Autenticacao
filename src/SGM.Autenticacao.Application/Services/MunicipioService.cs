using AutoMapper;
using SGM.Autenticacao.Domain.Dto;
using SGM.Autenticacao.Domain.Entities;
using SGM.Autenticacao.Domain.Interfaces.Repository;
using SGM.Autenticacao.Domain.Interfaces.Services;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGM.Autenticacao.Application.Services
{
    public class MunicipioService : IMunicipioService
    {             
        private readonly IMunicipioRepository _municipioRepository;
        private readonly IMapper _mapper;

        public MunicipioService(IMunicipioRepository municipioRepository, IMapper mapper)
        {
            _municipioRepository = municipioRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<MunicipioDto>>> ObterTodosPorEstado(int estadoId)
        {
            var municipios = await _municipioRepository.Find(x => x.EstadoId == estadoId);

            if (!municipios.Any())
                return ResultDto<IEnumerable<MunicipioDto>>.Validation("Município não encontrado na base de dados!");

            var municipioDto = _mapper.Map<IEnumerable<Municipio>, IEnumerable<MunicipioDto>>(municipios);          

            return await Task.FromResult(ResultDto<IEnumerable<MunicipioDto>>.Success(municipioDto));
        }     
    }
}
