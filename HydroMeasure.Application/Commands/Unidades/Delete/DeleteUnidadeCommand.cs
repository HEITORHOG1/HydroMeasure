using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Unidades.Delete
{
    public class DeleteUnidadeCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }

        public DeleteUnidadeCommand(Guid id)
        {
            Id = id;
        }
    }
}