using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Relatorios.GetByCondominio
{
    public class GetRelatoriosByCondominioQueryHandler : IRequestHandler<GetRelatoriosByCondominioQuery, IEnumerable<RelatorioDto>>
    {
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly IMapper _mapper;

        public GetRelatoriosByCondominioQueryHandler(IRelatorioRepository relatorioRepository, IMapper mapper)
        {
            _relatorioRepository = relatorioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RelatorioDto>> Handle(GetRelatoriosByCondominioQuery request, CancellationToken cancellationToken)
        {
            var relatorios = await _relatorioRepository.GetRelatoriosByCondominioAsync(request.CondominioId);
            return _mapper.Map<IEnumerable<RelatorioDto>>(relatorios);
        }
    }
}