using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Leituras.Delete
{
    public class DeleteLeituraCommandHandler : IRequestHandler<DeleteLeituraCommand, OperationResult<bool>>
    {
        private readonly ILeituraRepository _leituraRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeituraCommandHandler(ILeituraRepository leituraRepository, IUnitOfWork unitOfWork)
        {
            _leituraRepository = leituraRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteLeituraCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar Leitura
            var leitura = await _leituraRepository.GetByIdAsync(request.Id);
            if (leitura == null)
            {
                return new OperationResult<bool>
                {
                    Success = false,
                    Message = "Leitura não encontrada.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Remover Leitura
            await _leituraRepository.Remove(leitura);

            // 3. Salvar Alterações
            await _unitOfWork.SaveChangesAsync();

            // 4. Retornar Sucesso
            return new OperationResult<bool>
            {
                Success = true,
                Data = true,
                Message = "Leitura removida com sucesso."
            };
        }
    }
}