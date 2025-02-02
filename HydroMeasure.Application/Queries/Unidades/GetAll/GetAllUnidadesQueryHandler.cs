using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Unidades.GetAll
{
    public class GetAllUnidadesQueryHandler : IRequestHandler<GetAllUnidadesQuery, IEnumerable<UnidadeDto>>
    {
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IMapper _mapper;

        public GetAllUnidadesQueryHandler(IUnidadeRepository unidadeRepository, IMapper mapper)
        {
            _unidadeRepository = unidadeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnidadeDto>> Handle(GetAllUnidadesQuery request, CancellationToken cancellationToken)
        {
            var unidades = await _unidadeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UnidadeDto>>(unidades);
        }
    }
}