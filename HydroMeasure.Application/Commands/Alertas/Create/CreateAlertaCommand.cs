using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Alertas.Create
{
    public class CreateAlertaCommand : IRequest<OperationResult<AlertaDto>>
    {
        public Guid UnidadeId { get; set; }
        public Guid CondominioId { get; set; }
        public string Tipo { get; set; } = string.Empty; // TipoAlerta como string
        public string Mensagem { get; set; } = string.Empty;
        public DateTime DataAlerta { get; set; } = DateTime.UtcNow; // Data padrão
    }
}