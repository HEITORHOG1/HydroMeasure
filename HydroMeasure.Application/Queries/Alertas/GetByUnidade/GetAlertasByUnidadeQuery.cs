using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Alertas.GetByUnidade
{
    public class GetAlertasByUnidadeQuery : IRequest<IEnumerable<AlertaDto>>
    {
        public Guid UnidadeId { get; }

        public GetAlertasByUnidadeQuery(Guid unidadeId)
        {
            UnidadeId = unidadeId;
        }
    }
}