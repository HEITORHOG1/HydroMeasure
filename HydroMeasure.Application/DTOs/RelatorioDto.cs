namespace HydroMeasure.Application.DTOs
{
    public record RelatorioDto(
         Guid Id,
         string Tipo,          // TipoRelatorio como string
         string Periodo,       // Ex: "2025-01", "2025-Q1", "2025-S1"
         string Titulo,        // Título do relatório
         string Formato,       // Formato do relatório (PDF, Excel, etc.)
         DateTime DataGeracao,
         Guid UsuarioId,
         string NomeUsuario    // Nome do usuário que gerou o relatório
     );
}