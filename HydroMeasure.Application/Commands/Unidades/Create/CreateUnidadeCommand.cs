using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Unidades.Create
{
    public class CreateUnidadeCommand : IRequest<OperationResult<UnidadeDto>>
    {
        public Guid CondominioId { get; set; }
        public string Identificacao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // TipoUnidade como string
        public string? MoradorResponsavel { get; set; }
        public decimal MediaConsumo { get; set; }
    }
}