using HydroMeasure.Domain.Entities;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Condominios.Update
{
    public class UpdateCondominioCommand : IRequest<OperationResult<Condominio>>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string? CNPJ { get; set; }
        public string? Sindico { get; set; }
        public string? TelefoneSindico { get; set; }
        public string? EmailSindico { get; set; }
        public bool Ativo { get; set; }
    }
}