using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Condominios.GetAll
{
    public class GetAllCondominiosQueryHandler : IRequestHandler<GetAllCondominiosQuery, IEnumerable<CondominioDto>>
    {
        private readonly ICondominioRepository _condominioRepository;
        private readonly IMapper _mapper;

        public GetAllCondominiosQueryHandler(ICondominioRepository condominioRepository, IMapper mapper)
        {
            _condominioRepository = condominioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CondominioDto>> Handle(GetAllCondominiosQuery request, CancellationToken cancellationToken)
        {
            var condominios = await _condominioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CondominioDto>>(condominios);
        }
    }
}