using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Hidrometros.Create
{
    public class CreateHidrometroCommandHandler : IRequestHandler<CreateHidrometroCommand, OperationResult<HidrometroDto>>
    {
        private readonly IHidrometroRepository _hidrometroRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHidrometroCommandHandler(IHidrometroRepository hidrometroRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _hidrometroRepository = hidrometroRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<HidrometroDto>> Handle(CreateHidrometroCommand request, CancellationToken cancellationToken)
        {
            // 1. Create Hidrometro Entity
            var hidrometro = new Hidrometro(
                request.UnidadeId,
                request.NumeroSerie,
                request.Modelo,
                request.DataInstalacao
            );

            // 2. Add Hidrometro to Repository
            await _hidrometroRepository.AddAsync(hidrometro);

            // 3. Save Changes to Database
            await _unitOfWork.SaveChangesAsync();

            // 4. Map Entity to DTO
            var hidrometroDto = _mapper.Map<HidrometroDto>(hidrometro);

            // 5. Return Success Result
            return new OperationResult<HidrometroDto>
            {
                Success = true,
                Data = hidrometroDto,
                Message = "Hidrômetro criado com sucesso."
            };
        }
    }
}