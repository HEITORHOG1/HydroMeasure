using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracoesCondominio.Update
{
    public class UpdateConfiguracaoCondominioCommandHandler : IRequestHandler<UpdateConfiguracaoCondominioCommand, OperationResult<ConfiguracaoCondominioDto>>
    {
        private readonly IConfiguracaoCondominioRepository _configuracaoCondominioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateConfiguracaoCondominioCommandHandler(IConfiguracaoCondominioRepository configuracaoCondominioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuracaoCondominioRepository = configuracaoCondominioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<ConfiguracaoCondominioDto>> Handle(UpdateConfiguracaoCondominioCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar a Configuração Existente
            var configuracaoCondominio = await _configuracaoCondominioRepository.GetByIdAsync(request.Id);
            if (configuracaoCondominio == null)
            {
                return new OperationResult<ConfiguracaoCondominioDto> // Substituído OperationResultHelper
                {
                    Success = false,
                    Message = "Configuração de condomínio não encontrada.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Validar se o CondominioId no Command corresponde ao CondominioId da Configuração Existente (se necessário)
            if (configuracaoCondominio.CondominioId != request.CondominioId)
            {
                return new OperationResult<ConfiguracaoCondominioDto> // Substituído OperationResultHelper
                {
                    Success = false,
                    Message = "CondominioId inconsistente.",
                    ErrorCode = "InvalidInput" // Ou "Conflict" dependendo da regra de negócio
                };
            }

            // 3. Converter Enums (strings do Command para Enums do Domínio)
            if (!Enum.TryParse<MetodoCalculoMedia>(request.MetodoCalculoMedia, true, out var metodoCalculoMedia))
            {
                return new OperationResult<ConfiguracaoCondominioDto> // Substituído OperationResultHelper
                {
                    Success = false,
                    Message = "Método de cálculo de média inválido.",
                    ErrorCode = "InvalidInput"
                };
            }

            if (!Enum.TryParse<FormatoRelatorio>(request.FormatoRelatorio, true, out var formatoRelatorio))
            {
                return new OperationResult<ConfiguracaoCondominioDto> // Substituído OperationResultHelper
                {
                    Success = false,
                    Message = "Formato de relatório inválido.",
                    ErrorCode = "InvalidInput"
                };
            }

            if (!Enum.TryParse<PeriodicidadeAlerta>(request.PeriodicidadeAlerta, true, out var periodicidadeAlerta))
            {
                return new OperationResult<ConfiguracaoCondominioDto> // Substituído OperationResultHelper
                {
                    Success = false,
                    Message = "Periodicidade de alerta inválida.",
                    ErrorCode = "InvalidInput"
                };
            }

            // 4. Atualizar as Propriedades da Entidade
            configuracaoCondominio.Update(
                metodoCalculoMedia,
                formatoRelatorio,
                periodicidadeAlerta,
                request.ValorMetroCubico
            );

            // 5. Salvar as Alterações no Banco de Dados
            await _unitOfWork.SaveChangesAsync();

            // 6. Mapear a Entidade Atualizada para DTO
            var configuracaoCondominioDto = _mapper.Map<ConfiguracaoCondominioDto>(configuracaoCondominio);

            // 7. Retornar Resultado de Sucesso
            return new OperationResult<ConfiguracaoCondominioDto> // Substituído OperationResultHelper
            {
                Success = true,
                Data = configuracaoCondominioDto,
                Message = "Configuração de condomínio atualizada com sucesso."
            };
        }
    }
}