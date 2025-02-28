using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Alertas.GetNaoResolvidos
{
    public class GetAlertasNaoResolvidosQueryHandler : IRequestHandler<GetAlertasNaoResolvidosQuery, IEnumerable<AlertaDto>>
    {
        private readonly IAlertaRepository _alertaRepository;
        private readonly IMapper _mapper;

        public GetAlertasNaoResolvidosQueryHandler(IAlertaRepository alertaRepository, IMapper mapper)
        {
            _alertaRepository = alertaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlertaDto>> Handle(GetAlertasNaoResolvidosQuery request, CancellationToken cancellationToken)
        {
            var alertas = await _alertaRepository.GetAlertasNaoResolvidosAsync(request.CondominioId);
            return _mapper.Map<IEnumerable<AlertaDto>>(alertas);
        }
    }
}