using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Hidrometros.Delete
{
    public class DeleteHidrometroCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }

        public DeleteHidrometroCommand(Guid id)
        {
            Id = id;
        }
    }
}