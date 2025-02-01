using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;

namespace HydroMeasure.Application.Mappings
{
    public class CondominioProfile : Profile
    {
        public CondominioProfile()
        {
            // Mapeia da entidade Condominio para o DTO CondominioDto
            CreateMap<Condominio, CondominioDto>();
        }
    }
}