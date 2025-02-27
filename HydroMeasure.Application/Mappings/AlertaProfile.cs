using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;

namespace HydroMeasure.Application.Mappings
{
    public class AlertaProfile : Profile
    {
        public AlertaProfile()
        {
            CreateMap<Alerta, AlertaDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()))
                .ForMember(dest => dest.IdentificacaoUnidade, opt => opt.MapFrom(src => src.Unidade.Identificacao))
                .ForMember(dest => dest.NomeUsuarioResolvido, opt => opt.MapFrom(src => src.UsuarioResolvido != null ? src.UsuarioResolvido.Nome : null));
        }
    }
}