using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Relatorios.GetByPeriodo
{
    public class GetRelatoriosByPeriodoQueryHandler : IRequestHandler<GetRelatoriosByPeriodoQuery, IEnumerable<RelatorioDto>>
    {
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly IMapper _mapper;

        public GetRelatoriosByPeriodoQueryHandler(IRelatorioRepository relatorioRepository, IMapper mapper)
        {
            _relatorioRepository = relatorioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RelatorioDto>> Handle(GetRelatoriosByPeriodoQuery request, CancellationToken cancellationToken)
        {
            var relatorios = await _relatorioRepository.GetRelatoriosByPeriodoAsync(request.Periodo);
            return _mapper.Map<IEnumerable<RelatorioDto>>(relatorios);
        }
    }
}