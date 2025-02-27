using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Alertas.Resolver
{
    public class ResolverAlertaCommand : IRequest<OperationResult<AlertaDto>>
    {
        public Guid Id { get; set; }
        public Guid UsuarioResolvidoId { get; set; }
    }
}