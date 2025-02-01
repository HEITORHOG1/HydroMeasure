using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Condominios.Delete
{
    public class DeleteCondominioCommandHandler : IRequestHandler<DeleteCondominioCommand, OperationResult<bool>>
    {
        private readonly ICondominioRepository _condominioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCondominioCommandHandler(ICondominioRepository condominioRepository, IUnitOfWork unitOfWork)
        {
            _condominioRepository = condominioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCondominioCommand request, CancellationToken cancellationToken)
        {
            var condominio = await _condominioRepository.GetByIdAsync(request.Id);
            if (condominio == null)
            {
                return new OperationResult<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Condomínio não encontrado."
                };
            }

            await _condominioRepository.Remove(condominio);
            await _unitOfWork.SaveChangesAsync();

            return new OperationResult<bool>
            {
                Success = true,
                Data = true,
                Message = "Condomínio removido com sucesso."
            };
        }
    }
}