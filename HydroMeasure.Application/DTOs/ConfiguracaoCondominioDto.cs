namespace HydroMeasure.Application.DTOs
{
    public record ConfiguracaoCondominioDto(
        Guid Id,
        Guid CondominioId,
        string MetodoCalculoMedia, // Enum MetodoCalculoMedia como string
        string FormatoRelatorio, // Enum FormatoRelatorio como string
        string PeriodicidadeAlerta, // Enum PeriodicidadeAlerta como string
        decimal ValorMetroCubico
    );
}