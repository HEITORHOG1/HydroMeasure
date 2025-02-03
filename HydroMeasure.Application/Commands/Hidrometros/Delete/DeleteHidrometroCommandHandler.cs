using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Hidrometros.Delete
{
    public class DeleteHidrometroCommandHandler : IRequestHandler<DeleteHidrometroCommand, OperationResult<bool>>
    {
        private readonly IHidrometroRepository _hidrometroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHidrometroCommandHandler(IHidrometroRepository hidrometroRepository, IUnitOfWork unitOfWork)
        {
            _hidrometroRepository = hidrometroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteHidrometroCommand request, CancellationToken cancellationToken)
        {
            // 1. Get Existing Hidrometro from Repository
            var hidrometro = await _hidrometroRepository.GetByIdAsync(request.Id);
            if (hidrometro == null)
            {
                return new OperationResult<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Hidrômetro não encontrado.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Remove Hidrometro from Repository
            await _hidrometroRepository.Remove(hidrometro);

            // 3. Save Changes to Database
            await _unitOfWork.SaveChangesAsync();

            // 4. Return Success Result
            return new OperationResult<bool>
            {
                Success = true,
                Data = true,
                Message = "Hidrômetro removido com sucesso."
            };
        }
    }
}
