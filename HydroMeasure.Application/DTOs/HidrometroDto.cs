namespace HydroMeasure.Application.DTOs
{
    public record HidrometroDto(
        Guid Id,
        Guid UnidadeId,
        string NumeroSerie,
        string? Modelo,
        DateTime? DataInstalacao,
        bool Ativo,
        string Status // StatusHidrometro como string (enum)
    );
}