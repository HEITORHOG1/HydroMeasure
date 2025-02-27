using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Relatorios.GetById
{
    public class GetRelatorioByIdQuery : IRequest<RelatorioDto?>
    {
        public Guid Id { get; }

        public GetRelatorioByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}