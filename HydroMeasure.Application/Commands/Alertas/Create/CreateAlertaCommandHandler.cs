using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Alertas.Create
{
    public class CreateAlertaCommandHandler : IRequestHandler<CreateAlertaCommand, OperationResult<AlertaDto>>
    {
        private readonly IAlertaRepository _alertaRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly ICondominioRepository _condominioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAlertaCommandHandler(
            IAlertaRepository alertaRepository,
            IUnidadeRepository unidadeRepository,
            ICondominioRepository condominioRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _alertaRepository = alertaRepository;
            _unidadeRepository = unidadeRepository;
            _condominioRepository = condominioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<AlertaDto>> Handle(CreateAlertaCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar se a Unidade existe
            var unidade = await _unidadeRepository.GetByIdAsync(request.UnidadeId);
            if (unidade == null)
            {
                return new OperationResult<AlertaDto>
                {
                    Success = false,
                    Message = "Unidade não encontrada.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Validar se o Condomínio existe
            var condominio = await _condominioRepository.GetByIdAsync(request.CondominioId);
            if (condominio == null)
            {
                return new OperationResult<AlertaDto>
                {
                    Success = false,
                    Message = "Condomínio não encontrado.",
                    ErrorCode = "NotFound"
                };
            }

            // 3. Validar se a Unidade pertence ao Condomínio
            if (unidade.CondominioId != request.CondominioId)
            {
                return new OperationResult<AlertaDto>
                {
                    Success = false,
                    Message = "A unidade não pertence ao condomínio informado.",
                    ErrorCode = "InvalidInput"
                };
            }

            // 4. Validar e converter TipoAlerta string para Enum
            if (!Enum.TryParse<TipoAlerta>(request.Tipo, true, out var tipoAlerta))
            {
                return new OperationResult<AlertaDto>
                {
                    Success = false,
                    Message = "Tipo de alerta inválido.",
                    ErrorCode = "InvalidInput"
                };
            }

            // 5. Criar entidade Alerta
            var alerta = new Alerta(
                request.UnidadeId,
                tipoAlerta,
                request.Mensagem,
                request.DataAlerta
            );

            // Definir o ID do condomínio
            alerta.CondominioId = request.CondominioId;

            // 6. Adicionar Alerta ao repositório
            await _alertaRepository.AddAsync(alerta);
            await _unitOfWork.SaveChangesAsync();

            // 7. Mapear para DTO e retornar
            var alertaDto = _mapper.Map<AlertaDto>(alerta);
            alertaDto = alertaDto with { IdentificacaoUnidade = unidade.Identificacao };

            return new OperationResult<AlertaDto>
            {
                Success = true,
                Data = alertaDto,
                Message = "Alerta criado com sucesso."
            };
        }
    }
}