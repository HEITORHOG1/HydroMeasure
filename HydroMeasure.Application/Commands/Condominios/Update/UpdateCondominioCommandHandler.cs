using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Condominios.Update
{
    public class UpdateCondominioCommandHandler : IRequestHandler<UpdateCondominioCommand, OperationResult<Condominio>>
    {
        private readonly ICondominioRepository _condominioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCondominioCommandHandler(ICondominioRepository condominioRepository, IUnitOfWork unitOfWork)
        {
            _condominioRepository = condominioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Condominio>> Handle(UpdateCondominioCommand request, CancellationToken cancellationToken)
        {
            var condominio = await _condominioRepository.GetByIdAsync(request.Id);
            if (condominio == null)
            {
                return new OperationResult<Condominio>
                {
                    Success = false,
                    Message = "Condomínio não encontrado."
                };
            }

            // Atualiza os dados da entidade
            condominio.Update(
                request.Nome,
                request.Endereco,
                request.CNPJ,
                request.Sindico,
                request.TelefoneSindico,
                request.EmailSindico,
                request.Ativo
            );

            await _unitOfWork.SaveChangesAsync();

            return new OperationResult<Condominio>
            {
                Success = true,
                Data = condominio,
                Message = "Condomínio atualizado com sucesso."
            };
        }
    }
}