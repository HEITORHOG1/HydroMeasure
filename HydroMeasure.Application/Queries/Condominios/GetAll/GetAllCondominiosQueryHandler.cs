using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Condominios.GetAll
{
    public class GetAllCondominiosQueryHandler : IRequestHandler<GetAllCondominiosQuery, IEnumerable<Condominio>>
    {
        private readonly ICondominioRepository _condominioRepository;

        public GetAllCondominiosQueryHandler(ICondominioRepository condominioRepository)
        {
            _condominioRepository = condominioRepository;
        }

        public async Task<IEnumerable<Condominio>> Handle(GetAllCondominiosQuery request, CancellationToken cancellationToken)
        {
            return await _condominioRepository.GetAllAsync();
        }
    }
}