using HydroMeasure.Domain.Entities;
using MediatR;

namespace HydroMeasure.Application.Queries.Condominios.GetAll
{
    public class GetAllCondominiosQuery : IRequest<IEnumerable<Condominio>>
    {
    }
}