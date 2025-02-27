using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Alertas.GetByCondominio
{
    public class GetAlertasByCondominioQuery : IRequest<IEnumerable<AlertaDto>>
    {
        public Guid CondominioId { get; }

        public GetAlertasByCondominioQuery(Guid condominioId)
        {
            CondominioId = condominioId;
        }
    }
}