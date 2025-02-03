using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Hidrometros.Update
{
    public class UpdateHidrometroCommandHandler : IRequestHandler<UpdateHidrometroCommand, OperationResult<HidrometroDto>>
    {
        private readonly IHidrometroRepository _hidrometroRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHidrometroCommandHandler(IHidrometroRepository hidrometroRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _hidrometroRepository = hidrometroRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<HidrometroDto>> Handle(UpdateHidrometroCommand request, CancellationToken cancellationToken)
        {
            // 1. Get Existing Hidrometro from Repository
            var hidrometro = await _hidrometroRepository.GetByIdAsync(request.Id);
            if (hidrometro == null)
            {
                return new OperationResult<HidrometroDto>
                {
                    Success = false,
                    Message = "Hidrômetro não encontrado.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Validate and Convert Status string to Enum
            if (!Enum.TryParse<StatusHidrometro>(request.Status, true, out var statusHidrometro))
            {
                return new OperationResult<HidrometroDto>
                {
                    Success = false,
                    Message = "Status de hidrômetro inválido.",
                    ErrorCode = "InvalidInput"
                };
            }

            // 3. Update Hidrometro Entity properties
            hidrometro.Update(
                request.UnidadeId, // Consider if UnidadeId should be updatable
                request.NumeroSerie, // Consider if NumeroSerie should be updatable
                request.Modelo,
                request.DataInstalacao,
                request.Ativo,
                statusHidrometro
            );

            // 4. Save Changes to Database
            await _unitOfWork.SaveChangesAsync();

            // 5. Map Updated Entity to DTO
            var hidrometroDto = _mapper.Map<HidrometroDto>(hidrometro);

            // 6. Return Success Result
            return new OperationResult<HidrometroDto>
            {
                Success = true,
                Data = hidrometroDto,
                Message = "Hidrômetro atualizado com sucesso."
            };
        }
    }
}