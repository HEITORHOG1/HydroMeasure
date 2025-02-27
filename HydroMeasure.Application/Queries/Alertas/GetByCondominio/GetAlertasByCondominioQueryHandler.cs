using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Alertas.GetByCondominio
{
    public class GetAlertasByCondominioQueryHandler : IRequestHandler<GetAlertasByCondominioQuery, IEnumerable<AlertaDto>>
    {
        private readonly IAlertaRepository _alertaRepository;
        private readonly IMapper _mapper;

        public GetAlertasByCondominioQueryHandler(IAlertaRepository alertaRepository, IMapper mapper)
        {
            _alertaRepository = alertaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlertaDto>> Handle(GetAlertasByCondominioQuery request, CancellationToken cancellationToken)
        {
            var alertas = await _alertaRepository.GetAlertasByCondominioAsync(request.CondominioId);
            return _mapper.Map<IEnumerable<AlertaDto>>(alertas);
        }
    }
}