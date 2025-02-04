using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracoesCondominio.Create
{
    public class CreateConfiguracaoCondominioCommand : IRequest<OperationResult<ConfiguracaoCondominioDto>>
    {
        public Guid CondominioId { get; set; }
        public string MetodoCalculoMedia { get; set; } = string.Empty; // Enum como string
        public string FormatoRelatorio { get; set; } = string.Empty; // Enum como string
        public string PeriodicidadeAlerta { get; set; } = string.Empty; // Enum como string
        public decimal ValorMetroCubico { get; set; }
    }
}