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
    public class EstadoService : IEstadoService
    {             
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMapper _mapper;

        public EstadoService(IEstadoRepository estadoRepository, IMapper mapper)
        {
            _estadoRepository = estadoRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<EstadoDto>>> ObterTodos()
        {
            var estados = await _estadoRepository.GetAll();

            if (!estados.Any())
                return ResultDto<IEnumerable<EstadoDto>>.Validation("Estados não encontrado na base de dados!");

            var estadoDto = _mapper.Map<IEnumerable<Estado>, IEnumerable<EstadoDto>>(estados);          

            return await Task.FromResult(ResultDto<IEnumerable<EstadoDto>>.Success(estadoDto));
        }
    }
}
