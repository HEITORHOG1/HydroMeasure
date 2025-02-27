using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Alertas.GetById
{
    public class GetAlertaByIdQuery : IRequest<AlertaDto?>
    {
        public Guid Id { get; }

        public GetAlertaByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}