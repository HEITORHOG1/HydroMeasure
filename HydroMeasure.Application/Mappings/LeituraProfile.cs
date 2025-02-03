using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;

namespace HydroMeasure.Application.Mappings
{
    public class LeituraProfile : Profile
    {
        public LeituraProfile()
        {
            CreateMap<Leitura, LeituraDto>().ReverseMap(); // Mapeamento bidirecional básico
            // Se precisar de mapeamentos mais complexos, configure aqui
        }
    }
}