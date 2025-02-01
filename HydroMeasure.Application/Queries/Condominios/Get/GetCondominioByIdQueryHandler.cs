using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Condominios.Get
{
    public class GetCondominioByIdQueryHandler : IRequestHandler<GetCondominioByIdQuery, Condominio?>
    {
        private readonly ICondominioRepository _condominioRepository;

        public GetCondominioByIdQueryHandler(ICondominioRepository condominioRepository)
        {
            _condominioRepository = condominioRepository;
        }

        public async Task<Condominio?> Handle(GetCondominioByIdQuery request, CancellationToken cancellationToken)
        {
            return await _condominioRepository.GetCondominioWithDetailsAsync(request.Id);
        }
    }
}