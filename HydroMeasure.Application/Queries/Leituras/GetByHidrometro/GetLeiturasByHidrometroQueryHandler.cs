using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Leituras.GetByHidrometro
{
    public class GetLeiturasByHidrometroQueryHandler : IRequestHandler<GetLeiturasByHidrometroQuery, IEnumerable<LeituraDto>>
    {
        private readonly ILeituraRepository _leituraRepository;
        private readonly IMapper _mapper;

        public GetLeiturasByHidrometroQueryHandler(ILeituraRepository leituraRepository, IMapper mapper)
        {
            _leituraRepository = leituraRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeituraDto>> Handle(GetLeiturasByHidrometroQuery request, CancellationToken cancellationToken)
        {
            var leituras = await _leituraRepository.GetLeiturasPorHidrometroAsync(request.HidrometroId);
            return _mapper.Map<IEnumerable<LeituraDto>>(leituras);
        }
    }
}