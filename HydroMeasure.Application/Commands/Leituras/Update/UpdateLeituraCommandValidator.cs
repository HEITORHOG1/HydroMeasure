using FluentValidation;

namespace HydroMeasure.Application.Commands.Leituras.Update
{
    public class UpdateLeituraCommandValidator : AbstractValidator<UpdateLeituraCommand>
    {
        public UpdateLeituraCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Id é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("Id inválido.");

            RuleFor(command => command.LeituraAtual)
                .GreaterThanOrEqualTo(0).WithMessage("LeituraAtual não pode ser negativa.");

            RuleFor(command => command.DataLeitura)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DataLeitura não pode ser no futuro.");
        }
    }
}