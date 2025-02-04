using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Application.Mappings
{
    public class ConfiguracaoCondominioProfile : Profile
    {
        public ConfiguracaoCondominioProfile()
        {
            CreateMap<ConfiguracaoCondominio, ConfiguracaoCondominioDto>()
                .ForMember(dest => dest.MetodoCalculoMedia, opt => opt.MapFrom(src => src.MetodoCalculoMedia.ToString())) // Enum para string
                .ForMember(dest => dest.FormatoRelatorio, opt => opt.MapFrom(src => src.FormatoRelatorio.ToString())) // Enum para string
                .ForMember(dest => dest.PeriodicidadeAlerta, opt => opt.MapFrom(src => src.PeriodicidadeAlerta.ToString())) // Enum para string
                .ReverseMap() // Permite mapear de DTO para Entidade também
                .ForMember(dest => dest.MetodoCalculoMedia, opt => opt.MapFrom(src => Enum.Parse<MetodoCalculoMedia>(src.MetodoCalculoMedia))) // String para Enum
                .ForMember(dest => dest.FormatoRelatorio, opt => opt.MapFrom(src => Enum.Parse<FormatoRelatorio>(src.FormatoRelatorio))) // String para Enum
                .ForMember(dest => dest.PeriodicidadeAlerta, opt => opt.MapFrom(src => Enum.Parse<PeriodicidadeAlerta>(src.PeriodicidadeAlerta))); // String para Enum
        }
    }
}