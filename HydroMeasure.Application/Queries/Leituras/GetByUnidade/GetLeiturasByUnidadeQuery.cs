using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Leituras.GetByUnidade
{
    public class GetLeiturasByUnidadeQuery : IRequest<IEnumerable<LeituraDto>>
    {
        public Guid UnidadeId { get; }

        public GetLeiturasByUnidadeQuery(Guid unidadeId)
        {
            UnidadeId = unidadeId;
        }
    }
}
