using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Leituras.GetByUnidade
{
    public class GetLeiturasByUnidadeQueryHandler : IRequestHandler<GetLeiturasByUnidadeQuery, IEnumerable<LeituraDto>>
    {
        private readonly ILeituraRepository _leituraRepository;
        private readonly IMapper _mapper;

        public GetLeiturasByUnidadeQueryHandler(ILeituraRepository leituraRepository, IMapper mapper)
        {
            _leituraRepository = leituraRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeituraDto>> Handle(GetLeiturasByUnidadeQuery request, CancellationToken cancellationToken)
        {
            var leituras = await _leituraRepository.GetLeiturasPorUnidadeAsync(request.UnidadeId);
            return _mapper.Map<IEnumerable<LeituraDto>>(leituras);
        }
    }
}
