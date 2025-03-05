using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.ConfiguracaoSistema
{
    public class GetConfiguracaoSistemaQuery : IRequest<ConfiguracaoSistemaDto?>
    {
    }
}