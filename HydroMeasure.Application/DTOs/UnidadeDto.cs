namespace HydroMeasure.Application.DTOs
{
    public record UnidadeDto(
        Guid Id,
        Guid CondominioId,
        string Identificacao,
        string Tipo, // Enum TipoUnidade como string
        string? MoradorResponsavel,
        decimal MediaConsumo,
        bool Ativo,
        string Status // Enum StatusUnidade como string
    );
}