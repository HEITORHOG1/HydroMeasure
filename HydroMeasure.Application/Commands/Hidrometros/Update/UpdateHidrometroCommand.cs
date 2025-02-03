using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Hidrometros.Update
{
    public class UpdateHidrometroCommand : IRequest<OperationResult<HidrometroDto>>
    {
        public Guid Id { get; set; }
        public Guid UnidadeId { get; set; } // Include UnidadeId for potential validation
        public string NumeroSerie { get; set; } = string.Empty;
        public string? Modelo { get; set; }
        public DateTime? DataInstalacao { get; set; }
        public bool Ativo { get; set; }
        public string Status { get; set; } = string.Empty; // StatusHidrometro as string
    }
}