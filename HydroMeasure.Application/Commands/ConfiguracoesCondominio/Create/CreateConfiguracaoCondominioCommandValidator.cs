using FluentValidation;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Application.Commands.ConfiguracoesCondominio.Create
{
    public class CreateConfiguracaoCondominioCommandValidator : AbstractValidator<CreateConfiguracaoCondominioCommand>
    {
        public CreateConfiguracaoCondominioCommandValidator()
        {
            RuleFor(command => command.CondominioId)
                .NotEmpty().WithMessage("CondominioId é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("CondominioId inválido.");

            RuleFor(command => command.MetodoCalculoMedia)
                .NotEmpty().WithMessage("Método de cálculo de média é obrigatório.")
                .Must(BeValidMetodoCalculoMedia).WithMessage("Método de cálculo de média inválido."); // Validação customizada para enum

            RuleFor(command => command.FormatoRelatorio)
                .NotEmpty().WithMessage("Formato de relatório é obrigatório.")
                .Must(BeValidFormatoRelatorio).WithMessage("Formato de relatório inválido."); // Validação customizada para enum

            RuleFor(command => command.PeriodicidadeAlerta)
                .NotEmpty().WithMessage("Periodicidade de alerta é obrigatória.")
                .Must(BeValidPeriodicidadeAlerta).WithMessage("Periodicidade de alerta inválida."); // Validação customizada para enum

            RuleFor(command => command.ValorMetroCubico)
                .GreaterThanOrEqualTo(0).WithMessage("Valor do metro cúbico não pode ser negativo.");
        }

        private bool BeValidMetodoCalculoMedia(string metodoCalculoMedia)
        {
            return Enum.TryParse<MetodoCalculoMedia>(metodoCalculoMedia, true, out _);
        }

        private bool BeValidFormatoRelatorio(string formatoRelatorio)
        {
            return Enum.TryParse<FormatoRelatorio>(formatoRelatorio, true, out _);
        }

        private bool BeValidPeriodicidadeAlerta(string periodicidadeAlerta)
        {
            return Enum.TryParse<PeriodicidadeAlerta>(periodicidadeAlerta, true, out _);
        }
    }
}