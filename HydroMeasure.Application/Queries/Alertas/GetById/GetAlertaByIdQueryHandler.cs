using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Alertas.GetById
{
    public class GetAlertaByIdQueryHandler : IRequestHandler<GetAlertaByIdQuery, AlertaDto?>
    {
        private readonly IAlertaRepository _alertaRepository;
        private readonly IMapper _mapper;

        public GetAlertaByIdQueryHandler(IAlertaRepository alertaRepository, IMapper mapper)
        {
            _alertaRepository = alertaRepository;
            _mapper = mapper;
        }

        public async Task<AlertaDto?> Handle(GetAlertaByIdQuery request, CancellationToken cancellationToken)
        {
            var alerta = await _alertaRepository.GetByIdAsync(request.Id);
            return alerta != null ? _mapper.Map<AlertaDto>(alerta) : null;
        }
    }
}