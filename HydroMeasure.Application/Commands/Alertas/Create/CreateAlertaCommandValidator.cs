using FluentValidation;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Application.Commands.Alertas.Create
{
    public class CreateAlertaCommandValidator : AbstractValidator<CreateAlertaCommand>
    {
        public CreateAlertaCommandValidator()
        {
            RuleFor(command => command.UnidadeId)
                .NotEmpty().WithMessage("ID da unidade é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID da unidade inválido.");

            RuleFor(command => command.CondominioId)
                .NotEmpty().WithMessage("ID do condomínio é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do condomínio inválido.");

            RuleFor(command => command.Tipo)
                .NotEmpty().WithMessage("Tipo de alerta é obrigatório.")
                .Must(BeValidTipoAlerta).WithMessage("Tipo de alerta inválido.");

            RuleFor(command => command.Mensagem)
                .NotEmpty().WithMessage("Mensagem é obrigatória.")
                .MaximumLength(500).WithMessage("Mensagem não pode exceder 500 caracteres.");

            RuleFor(command => command.DataAlerta)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data do alerta não pode ser no futuro.");
        }

        private bool BeValidTipoAlerta(string tipoAlerta)
        {
            return Enum.TryParse<TipoAlerta>(tipoAlerta, true, out _);
        }
    }
}