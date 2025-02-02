using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Application.Mappings
{
    public class UnidadeProfile : Profile
    {
        public UnidadeProfile()
        {
            CreateMap<Unidade, UnidadeDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString())) // Mapeia Enum para string
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); // Mapeia Enum para string
            CreateMap<UnidadeDto, Unidade>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => Enum.Parse<TipoUnidade>(src.Tipo))) // Mapeia string de volta para Enum
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<StatusUnidade>(src.Status))); // Mapeia string de volta para Enum
        }
    }
}