using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Leituras.GetByHidrometro
{
    public class GetLeiturasByHidrometroQuery : IRequest<IEnumerable<LeituraDto>>
    {
        public Guid HidrometroId { get; }

        public GetLeiturasByHidrometroQuery(Guid hidrometroId)
        {
            HidrometroId = hidrometroId;
        }
    }
}