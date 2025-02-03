using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Hidrometros.GetAll
{
    public class GetAllHidrometrosQuery : IRequest<IEnumerable<HidrometroDto>>
    {
    }
}
