using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Relatorios.GetById
{
    public class GetRelatorioByIdQueryHandler : IRequestHandler<GetRelatorioByIdQuery, RelatorioDto?>
    {
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly IMapper _mapper;

        public GetRelatorioByIdQueryHandler(IRelatorioRepository relatorioRepository, IMapper mapper)
        {
            _relatorioRepository = relatorioRepository;
            _mapper = mapper;
        }

        public async Task<RelatorioDto?> Handle(GetRelatorioByIdQuery request, CancellationToken cancellationToken)
        {
            var relatorio = await _relatorioRepository.GetByIdAsync(request.Id);
            return relatorio != null ? _mapper.Map<RelatorioDto>(relatorio) : null;
        }
    }
}