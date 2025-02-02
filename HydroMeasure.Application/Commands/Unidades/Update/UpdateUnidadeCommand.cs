using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Unidades.Update
{
    public class UpdateUnidadeCommand : IRequest<OperationResult<UnidadeDto>>
    {
        public Guid Id { get; set; }
        public Guid CondominioId { get; set; } // Pode ser útil para validação, se necessário
        public string Identificacao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // TipoUnidade como string
        public string? MoradorResponsavel { get; set; }
        public decimal MediaConsumo { get; set; }
        public bool Ativo { get; set; }
        public string Status { get; set; } = string.Empty; // StatusUnidade como string
    }
}