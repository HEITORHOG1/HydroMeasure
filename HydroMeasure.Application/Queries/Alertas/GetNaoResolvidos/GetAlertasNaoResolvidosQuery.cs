using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Alertas.GetNaoResolvidos
{
    public class GetAlertasNaoResolvidosQuery : IRequest<IEnumerable<AlertaDto>>
    {
        public Guid CondominioId { get; }

        public GetAlertasNaoResolvidosQuery(Guid condominioId)
        {
            CondominioId = condominioId;
        }
    }
}