using FluentValidation;
using HydroMeasure.Application.Commands.Leituras.Create;

namespace HydroMeasure.Application.Commands.Hidrometros.Create
{
    public class CreateLeituraCommandValidator : AbstractValidator<CreateLeituraCommand>
    {
        public CreateLeituraCommandValidator()
        {
            RuleFor(command => command.HidrometroId)
                .NotEmpty().WithMessage("HidrometroId é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("HidrometroId inválido.");

            RuleFor(command => command.LeituraAtual)
                .GreaterThanOrEqualTo(0).WithMessage("LeituraAtual não pode ser negativa.");

            RuleFor(command => command.DataLeitura)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DataLeitura não pode ser no futuro.");
        }
    }
}
