using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Alertas.GetByUnidade
{
    public class GetAlertasByUnidadeQueryHandler : IRequestHandler<GetAlertasByUnidadeQuery, IEnumerable<AlertaDto>>
    {
        private readonly IAlertaRepository _alertaRepository;
        private readonly IMapper _mapper;

        public GetAlertasByUnidadeQueryHandler(IAlertaRepository alertaRepository, IMapper mapper)
        {
            _alertaRepository = alertaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlertaDto>> Handle(GetAlertasByUnidadeQuery request, CancellationToken cancellationToken)
        {
            var alertas = await _alertaRepository.GetAlertasByUnidadeAsync(request.UnidadeId);
            return _mapper.Map<IEnumerable<AlertaDto>>(alertas);
        }
    }
}