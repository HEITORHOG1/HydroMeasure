using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Condominios.Update
{
    public class UpdateCondominioCommandHandler : IRequestHandler<UpdateCondominioCommand, OperationResult<CondominioDto>>
    {
        private readonly ICondominioRepository _condominioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCondominioCommandHandler(
            ICondominioRepository condominioRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _condominioRepository = condominioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<CondominioDto>> Handle(UpdateCondominioCommand request, CancellationToken cancellationToken)
        {
            var condominio = await _condominioRepository.GetByIdAsync(request.Id);
            if (condominio == null)
            {
                return new OperationResult<CondominioDto>
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

            // Mapeia a entidade atualizada para o DTO
            var condominioDto = _mapper.Map<CondominioDto>(condominio);

            return new OperationResult<CondominioDto>
            {
                Success = true,
                Data = condominioDto,
                Message = "Condomínio atualizado com sucesso."
            };
        }
    }
}