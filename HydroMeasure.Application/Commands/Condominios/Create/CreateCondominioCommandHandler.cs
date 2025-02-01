using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Condominios.Create
{
    public class CreateCondominioCommandHandler : IRequestHandler<CreateCondominioCommand, OperationResult<Condominio>>
    {
        private readonly ICondominioRepository _condominioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCondominioCommandHandler(ICondominioRepository condominioRepository, IUnitOfWork unitOfWork)
        {
            _condominioRepository = condominioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Condominio>> Handle(CreateCondominioCommand request, CancellationToken cancellationToken)
        {
            var condominio = new Condominio(
                request.Nome,
                request.Endereco,
                request.CNPJ,
                request.Sindico,
                request.TelefoneSindico,
                request.EmailSindico
            );

            await _condominioRepository.AddAsync(condominio);
            await _unitOfWork.SaveChangesAsync();

            return new OperationResult<Condominio>
            {
                Success = true,
                Data = condominio,
                Message = "Condomínio criado com sucesso."
            };
        }
    }
}