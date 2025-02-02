using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Unidades.GetById
{
    public class GetUnidadeByIdQuery : IRequest<UnidadeDto?>
    {
        public Guid Id { get; }

        public GetUnidadeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}