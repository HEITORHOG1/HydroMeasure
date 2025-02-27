using FluentValidation;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Application.Commands.Relatorios.Create
{
    public class GerarRelatorioCommandValidator : AbstractValidator<GerarRelatorioCommand>
    {
        public GerarRelatorioCommandValidator()
        {
            RuleFor(command => command.Tipo)
                .NotEmpty().WithMessage("Tipo de relatório é obrigatório.")
                .Must(BeValidTipoRelatorio).WithMessage("Tipo de relatório inválido.");

            RuleFor(command => command.Periodo)
                .NotEmpty().WithMessage("Período é obrigatório.")
                .Matches(@"^\d{4}-(0[1-9]|1[0-2])|^\d{4}-Q[1-4]|^\d{4}-S[1-2]$")
                .WithMessage("Formato de período inválido. Use YYYY-MM, YYYY-Q# ou YYYY-S#.");

            RuleFor(command => command.CondominioId)
                .NotEmpty().WithMessage("ID do condomínio é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do condomínio inválido.");

            RuleFor(command => command.UsuarioId)
                .NotEmpty().WithMessage("ID do usuário é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do usuário inválido.");

            RuleFor(command => command.Formato)
                .NotEmpty().WithMessage("Formato do relatório é obrigatório.");
        }

        private bool BeValidTipoRelatorio(string tipoRelatorio)
        {
            return Enum.TryParse<TipoRelatorio>(tipoRelatorio, true, out _);
        }
    }
}