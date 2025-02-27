using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Relatorios.Create
{
    public class GerarRelatorioCommand : IRequest<OperationResult<RelatorioDto>>
    {
        public string Tipo { get; set; } = string.Empty; // TipoRelatorio como string
        public string Periodo { get; set; } = string.Empty; // Ex: "2025-01" (mensal), "2025-Q1" (trimestral)
        public Guid CondominioId { get; set; }
        public Guid? UnidadeId { get; set; } // Opcional, se for relatório específico de unidade
        public Guid UsuarioId { get; set; } // Usuário que está gerando o relatório
        public string Formato { get; set; } = string.Empty; // Formato do relatório
    }
}