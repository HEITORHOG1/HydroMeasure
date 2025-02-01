namespace HydroMeasure.Application.DTOs
{
    public record CondominioDto(
        Guid Id,
        string Nome,
        string Endereco,
        string? CNPJ,
        string? Sindico,
        string? TelefoneSindico,
        string? EmailSindico,
        bool Ativo
    );
}