using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracoesCondominio.Update
{
    public class UpdateConfiguracaoCondominioCommand : IRequest<OperationResult<ConfiguracaoCondominioDto>>
    {
        public Guid Id { get; set; } // ID da Configuração a ser atualizada
        public Guid CondominioId { get; set; } // Incluí CondominioId para garantir que não está mudando de condomínio incorretamente (validação)
        public string MetodoCalculoMedia { get; set; } = string.Empty;
        public string FormatoRelatorio { get; set; } = string.Empty;
        public string PeriodicidadeAlerta { get; set; } = string.Empty;
        public decimal ValorMetroCubico { get; set; }
    }
}