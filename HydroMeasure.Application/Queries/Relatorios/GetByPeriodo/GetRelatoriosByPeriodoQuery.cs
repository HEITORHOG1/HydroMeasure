using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Relatorios.GetByPeriodo
{
    public class GetRelatoriosByPeriodoQuery : IRequest<IEnumerable<RelatorioDto>>
    {
        public string Periodo { get; }

        public GetRelatoriosByPeriodoQuery(string periodo)
        {
            Periodo = periodo;
        }
    }
}