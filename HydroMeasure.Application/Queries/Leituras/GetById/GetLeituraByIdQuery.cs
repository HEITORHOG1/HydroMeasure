using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Leituras.GetById
{
    public class GetLeituraByIdQuery : IRequest<LeituraDto?>
    {
        public Guid Id { get; }

        public GetLeituraByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}