namespace HydroMeasure.Application.DTOs
{
    public record AlertaDto(
         Guid Id,
         Guid UnidadeId,
         Guid CondominioId,
         string IdentificacaoUnidade,
         string Tipo,           // TipoAlerta como string
         string Mensagem,
         DateTime DataAlerta,
         bool Resolvido,
         Guid? UsuarioResolvidoId,
         string? NomeUsuarioResolvido
     );
}