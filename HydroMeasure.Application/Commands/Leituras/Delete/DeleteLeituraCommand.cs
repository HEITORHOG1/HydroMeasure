using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Leituras.Delete
{
    public class DeleteLeituraCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }

        public DeleteLeituraCommand(Guid id)
        {
            Id = id;
        }
    }
}
