using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Hidrometros.Create
{
    public class CreateHidrometroCommand : IRequest<OperationResult<HidrometroDto>>
    {
        public Guid UnidadeId { get; set; }
        public string NumeroSerie { get; set; } = string.Empty;
        public string? Modelo { get; set; }
        public DateTime? DataInstalacao { get; set; }
    }
}
