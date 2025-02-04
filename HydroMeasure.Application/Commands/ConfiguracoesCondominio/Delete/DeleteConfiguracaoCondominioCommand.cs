using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracoesCondominio.Delete
{
    public class DeleteConfiguracaoCondominioCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; }

        public DeleteConfiguracaoCondominioCommand(Guid id)
        {
            Id = id;
        }
    }
}