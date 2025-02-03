using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Leituras.GetById
{
    public class GetLeituraByIdQueryHandler : IRequestHandler<GetLeituraByIdQuery, LeituraDto?>
    {
        private readonly ILeituraRepository _leituraRepository;
        private readonly IMapper _mapper;

        public GetLeituraByIdQueryHandler(ILeituraRepository leituraRepository, IMapper mapper)
        {
            _leituraRepository = leituraRepository;
            _mapper = mapper;
        }

        public async Task<LeituraDto?> Handle(GetLeituraByIdQuery request, CancellationToken cancellationToken)
        {
            var leitura = await _leituraRepository.GetLeituraComDetalhesAsync(request.Id); // Usando GetLeituraComDetalhesAsync para trazer detalhes (Hidrometro, Unidade) se precisar
            return leitura is not null ? _mapper.Map<LeituraDto>(leitura) : null;
        }
    }
}
