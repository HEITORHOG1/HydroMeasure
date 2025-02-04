using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracoesCondominio.Delete
{
    public class DeleteConfiguracaoCondominioCommandHandler : IRequestHandler<DeleteConfiguracaoCondominioCommand, OperationResult<bool>>
    {
        private readonly IConfiguracaoCondominioRepository _configuracaoCondominioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteConfiguracaoCondominioCommandHandler(IConfiguracaoCondominioRepository configuracaoCondominioRepository, IUnitOfWork unitOfWork)
        {
            _configuracaoCondominioRepository = configuracaoCondominioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteConfiguracaoCondominioCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar a Configuração Existente
            var configuracaoCondominio = await _configuracaoCondominioRepository.GetByIdAsync(request.Id);
            if (configuracaoCondominio == null)
            {
                return new OperationResult<bool>
                {
                    Success = false,
                    Message = "Configuração de condomínio não encontrada.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Remover a Configuração do Repositório
            await _configuracaoCondominioRepository.Remove(configuracaoCondominio);

            // 3. Salvar as Alterações no Banco de Dados
            await _unitOfWork.SaveChangesAsync();

            // 4. Retornar Resultado de Sucesso
            return new OperationResult<bool>
            {
                Success = true,
                Data = true,
                Message = "Configuração de condomínio removida com sucesso."
            };
        }
    }
}