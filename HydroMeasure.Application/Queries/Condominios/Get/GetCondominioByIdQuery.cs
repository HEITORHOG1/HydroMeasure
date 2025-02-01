using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Condominios.Get
{
    public class GetCondominioByIdQuery : IRequest<CondominioDto?>
    {
        public Guid Id { get; }

        public GetCondominioByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}