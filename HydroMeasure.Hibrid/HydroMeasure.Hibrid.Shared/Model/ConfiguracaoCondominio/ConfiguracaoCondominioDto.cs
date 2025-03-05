namespace HydroMeasure.Hibrid.Shared.Model.ConfiguracaoCondominio
{
    public class ConfiguracaoCondominioDto
    {
        public Guid Id { get; set; }
        public Guid CondominioId { get; set; }
        public string MetodoCalculoMedia { get; set; } = string.Empty;
        public string FormatoRelatorio { get; set; } = string.Empty;
        public string PeriodicidadeAlerta { get; set; } = string.Empty;
        public decimal ValorMetroCubico { get; set; }

        public ConfiguracaoCondominioDto(Guid id, Guid condominioId, string metodoCalculoMedia, string formatoRelatorio, string periodicidadeAlerta, decimal valorMetroCubico)
        {
            Id = id;
            CondominioId = condominioId;
            MetodoCalculoMedia = metodoCalculoMedia;
            FormatoRelatorio = formatoRelatorio;
            PeriodicidadeAlerta = periodicidadeAlerta;
            ValorMetroCubico = valorMetroCubico;
        }
    }
}