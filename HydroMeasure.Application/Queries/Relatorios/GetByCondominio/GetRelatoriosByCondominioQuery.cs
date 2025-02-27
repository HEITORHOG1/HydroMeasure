using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Relatorios.GetByCondominio
{
    public class GetRelatoriosByCondominioQuery : IRequest<IEnumerable<RelatorioDto>>
    {
        public Guid CondominioId { get; }

        public GetRelatoriosByCondominioQuery(Guid condominioId)
        {
            CondominioId = condominioId;
        }
    }
}