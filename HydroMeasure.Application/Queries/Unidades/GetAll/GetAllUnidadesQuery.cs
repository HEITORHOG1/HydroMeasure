using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Unidades.GetAll
{
    public class GetAllUnidadesQuery : IRequest<IEnumerable<UnidadeDto>>
    {
    }
}