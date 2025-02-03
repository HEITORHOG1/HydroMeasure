using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Hidrometros.GetAll
{
    public class GetAllHidrometrosQueryHandler : IRequestHandler<GetAllHidrometrosQuery, IEnumerable<HidrometroDto>>
    {
        private readonly IHidrometroRepository _hidrometroRepository;
        private readonly IMapper _mapper;

        public GetAllHidrometrosQueryHandler(IHidrometroRepository hidrometroRepository, IMapper mapper)
        {
            _hidrometroRepository = hidrometroRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HidrometroDto>> Handle(GetAllHidrometrosQuery request, CancellationToken cancellationToken)
        {
            var hidrometros = await _hidrometroRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HidrometroDto>>(hidrometros);
        }
    }
}
