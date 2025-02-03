namespace HydroMeasure.Application.DTOs
{
    public record LeituraDto(
       Guid Id,
       Guid HidrometroId,
       Guid UnidadeId,
       decimal LeituraAtual,
       DateTime DataLeitura,
       decimal Consumo
   // Pode adicionar mais propriedades se necessário, como LeituraAnteriorId, etc.
   );
}