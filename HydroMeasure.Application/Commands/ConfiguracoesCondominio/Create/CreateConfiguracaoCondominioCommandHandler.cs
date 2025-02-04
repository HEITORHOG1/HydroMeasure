using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracoesCondominio.Create
{
    public class CreateConfiguracaoCondominioCommandHandler : IRequestHandler<CreateConfiguracaoCondominioCommand, OperationResult<ConfiguracaoCondominioDto>>
    {
        private readonly IConfiguracaoCondominioRepository _configuracaoCondominioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateConfiguracaoCondominioCommandHandler(IConfiguracaoCondominioRepository configuracaoCondominioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuracaoCondominioRepository = configuracaoCondominioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<ConfiguracaoCondominioDto>> Handle(CreateConfiguracaoCondominioCommand request, CancellationToken cancellationToken)
        {
            // 1. Converter Enums (strings do Command para Enums do Domínio)
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

            // 2. Criar Entidade ConfiguracaoCondominio
            var configuracaoCondominio = new ConfiguracaoCondominio(
                request.CondominioId,
                metodoCalculoMedia,
                formatoRelatorio,
                periodicidadeAlerta,
                request.ValorMetroCubico
            );

            // 3. Adicionar a Configuração ao Repositório
            await _configuracaoCondominioRepository.AddAsync(configuracaoCondominio);

            // 4. Salvar as Alterações no Banco de Dados
            await _unitOfWork.SaveChangesAsync();

            // 5. Mapear a Entidade para DTO
            var configuracaoCondominioDto = _mapper.Map<ConfiguracaoCondominioDto>(configuracaoCondominio);

            // 6. Retornar Resultado de Sucesso
            return new OperationResult<ConfiguracaoCondominioDto> // Substituído OperationResultHelper
            {
                Success = true,
                Data = configuracaoCondominioDto,
                Message = "Configuração de condomínio criada com sucesso."
            };
        }
    }
}