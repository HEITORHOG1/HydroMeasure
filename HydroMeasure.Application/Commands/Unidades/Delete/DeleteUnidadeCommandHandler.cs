using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Unidades.Delete
{
    public class DeleteUnidadeCommandHandler : IRequestHandler<DeleteUnidadeCommand, OperationResult<bool>>
    {
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUnidadeCommandHandler(IUnidadeRepository unidadeRepository, IUnitOfWork unitOfWork)
        {
            _unidadeRepository = unidadeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteUnidadeCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar a Unidade Existente pelo ID
            var unidade = await _unidadeRepository.GetByIdAsync(request.Id);
            if (unidade == null)
            {
                return new OperationResult<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Unidade não encontrada.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Remover a Unidade do Repositório
            await _unidadeRepository.Remove(unidade);

            // 3. Salvar as Alterações no Banco de Dados
            await _unitOfWork.SaveChangesAsync();

            // 4. Retornar Resultado de Sucesso
            return new OperationResult<bool>
            {
                Success = true,
                Data = true,
                Message = "Unidade removida com sucesso."
            };
        }
    }
}