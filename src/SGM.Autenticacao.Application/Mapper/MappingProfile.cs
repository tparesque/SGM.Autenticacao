using AutoMapper;
using SGM.Autenticacao.Domain.Dto;
using SGM.Autenticacao.Domain.Entities;

namespace SGM.Autenticacao.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")));
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<Estado, EstadoDto>();
            CreateMap<EstadoDto, Estado>();

            CreateMap<Municipio, MunicipioDto>();
            CreateMap<MunicipioDto, Municipio>();

            CreateMap<Endereco, EnderecoDto>()
               .ForMember(d => d.EstadoId, dto => dto.MapFrom(s => s.Municipio.EstadoId))
               .ForMember(d => d.EstadoNome, dto => dto.MapFrom(s => s.Municipio.Estado.Nome))
               .ForMember(d => d.MunicipioNome, dto => dto.MapFrom(s => s.Municipio.Nome));
            CreateMap<EnderecoDto, Endereco>();

        }
    }
}
