using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Unidades.GetById
{
    public class GetUnidadeByIdQueryHandler : IRequestHandler<GetUnidadeByIdQuery, UnidadeDto?>
    {
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IMapper _mapper;

        public GetUnidadeByIdQueryHandler(IUnidadeRepository unidadeRepository, IMapper mapper)
        {
            _unidadeRepository = unidadeRepository;
            _mapper = mapper;
        }

        public async Task<UnidadeDto?> Handle(GetUnidadeByIdQuery request, CancellationToken cancellationToken)
        {
            var unidade = await _unidadeRepository.GetUnidadeWithDetailsAsync(request.Id); // Usando GetUnidadeWithDetailsAsync para buscar entidades relacionadas se precisar
            return unidade is not null ? _mapper.Map<UnidadeDto>(unidade) : null;
        }
    }
}