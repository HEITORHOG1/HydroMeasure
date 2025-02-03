using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Hidrometros.GetById
{
    public class GetHidrometroByIdQuery : IRequest<HidrometroDto?>
    {
        public Guid Id { get; }

        public GetHidrometroByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}