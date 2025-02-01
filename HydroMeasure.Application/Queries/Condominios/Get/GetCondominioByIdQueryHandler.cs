using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Condominios.Get
{
    public class GetCondominioByIdQueryHandler : IRequestHandler<GetCondominioByIdQuery, CondominioDto?>
    {
        private readonly ICondominioRepository _condominioRepository;
        private readonly IMapper _mapper;

        public GetCondominioByIdQueryHandler(ICondominioRepository condominioRepository, IMapper mapper)
        {
            _condominioRepository = condominioRepository;
            _mapper = mapper;
        }

        public async Task<CondominioDto?> Handle(GetCondominioByIdQuery request, CancellationToken cancellationToken)
        {
            var condominio = await _condominioRepository.GetCondominioWithDetailsAsync(request.Id);
            return condominio is not null ? _mapper.Map<CondominioDto>(condominio) : null;
        }
    }
}