using HydroMeasure.Domain.Entities;
using MediatR;

namespace HydroMeasure.Application.Queries.Condominios.Get
{
    public class GetCondominioByIdQuery : IRequest<Condominio?>
    {
        public Guid Id { get; }

        public GetCondominioByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}