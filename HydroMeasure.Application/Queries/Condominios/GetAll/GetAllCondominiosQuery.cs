using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.Condominios.GetAll
{
    public class GetAllCondominiosQuery : IRequest<IEnumerable<CondominioDto>>
    {
    }
}