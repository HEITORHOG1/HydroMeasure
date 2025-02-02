using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Unidades.Update
{
    public class UpdateUnidadeCommandHandler : IRequestHandler<UpdateUnidadeCommand, OperationResult<UnidadeDto>>
    {
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUnidadeCommandHandler(IUnidadeRepository unidadeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unidadeRepository = unidadeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<UnidadeDto>> Handle(UpdateUnidadeCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar a Unidade Existente pelo ID
            var unidade = await _unidadeRepository.GetByIdAsync(request.Id);
            if (unidade == null)
            {
                return new OperationResult<UnidadeDto>
                {
                    Success = false,
                    Message = "Unidade não encontrada.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Validar e Converter TipoUnidade string para Enum
            if (!Enum.TryParse<TipoUnidade>(request.Tipo, true, out var tipoUnidade))
            {
                return new OperationResult<UnidadeDto>
                {
                    Success = false,
                    Message = "Tipo de unidade inválido.",
                    ErrorCode = "InvalidInput"
                };
            }

            // 3. Validar e Converter StatusUnidade string para Enum
            if (!Enum.TryParse<StatusUnidade>(request.Status, true, out var statusUnidade))
            {
                return new OperationResult<UnidadeDto>
                {
                    Success = false,
                    Message = "Status de unidade inválido.",
                    ErrorCode = "InvalidInput"
                };
            }

            // 4. Atualizar as Propriedades da Entidade Unidade
            unidade.Update(
                request.CondominioId, // Mantém o CondominioId original (ou pode validar se deve ser alterado)
                request.Identificacao,
                tipoUnidade,
                request.MoradorResponsavel,
                request.MediaConsumo,
                request.Ativo,
                statusUnidade
            );

            // 5. Salvar as Alterações no Banco de Dados
            await _unitOfWork.SaveChangesAsync();

            // 6. Mapear a Entidade Atualizada para DTO
            var unidadeDto = _mapper.Map<UnidadeDto>(unidade);

            // 7. Retornar Resultado de Sucesso
            return new OperationResult<UnidadeDto>
            {
                Success = true,
                Data = unidadeDto,
                Message = "Unidade atualizada com sucesso."
            };
        }
    }
}