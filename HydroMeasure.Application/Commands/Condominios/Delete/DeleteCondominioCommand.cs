using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Condominios.Delete
{
    public class DeleteCondominioCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }

        public DeleteCondominioCommand(Guid id)
        {
            Id = id;
        }
    }
}