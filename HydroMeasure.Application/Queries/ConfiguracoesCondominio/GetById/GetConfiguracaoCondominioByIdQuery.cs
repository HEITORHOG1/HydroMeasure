using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.ConfiguracoesCondominio.GetById
{
    public class GetConfiguracaoCondominioByIdQuery : IRequest<ConfiguracaoCondominioDto?>
    {
        public Guid Id { get; }

        public GetConfiguracaoCondominioByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}