using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Application.Mappings
{
    public class HidrometroProfile : Profile
    {
        public HidrometroProfile()
        {
            CreateMap<Hidrometro, HidrometroDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); // Mapeia Enum StatusHidrometro para string
            CreateMap<HidrometroDto, Hidrometro>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<StatusHidrometro>(src.Status))); // Mapeia string de volta para Enum StatusHidrometro
        }
    }
}
