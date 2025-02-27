using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;

namespace HydroMeasure.Application.Mappings
{
    public class RelatorioProfile : Profile
    {
        public RelatorioProfile()
        {
            CreateMap<Relatorio, RelatorioDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()))
                .ForMember(dest => dest.DataGeracao, opt => opt.MapFrom(src => src.DataCadastro))
                .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(dest => dest.Formato, opt => opt.MapFrom(src => "PDF")); // Valor padrão
        }
    }
}