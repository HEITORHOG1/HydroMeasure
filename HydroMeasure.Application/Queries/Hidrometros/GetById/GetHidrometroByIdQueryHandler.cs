using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.Hidrometros.GetById
{
    public class GetHidrometroByIdQueryHandler : IRequestHandler<GetHidrometroByIdQuery, HidrometroDto?>
    {
        private readonly IHidrometroRepository _hidrometroRepository;
        private readonly IMapper _mapper;

        public GetHidrometroByIdQueryHandler(IHidrometroRepository hidrometroRepository, IMapper mapper)
        {
            _hidrometroRepository = hidrometroRepository;
            _mapper = mapper;
        }

        public async Task<HidrometroDto?> Handle(GetHidrometroByIdQuery request, CancellationToken cancellationToken)
        {
            var hidrometro = await _hidrometroRepository.GetHidrometroWithDetailsAsync(request.Id);
            return hidrometro is not null ? _mapper.Map<HidrometroDto>(hidrometro) : null;
        }
    }
}