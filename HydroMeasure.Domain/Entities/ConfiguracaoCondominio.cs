using HydroMeasure.Domain.Entities.Base;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Domain.Entities
{
    public class ConfiguracaoCondominio : EntityBase // Herda de EntityBase
    {
        public Guid CondominioId { get; private set; } // FK para Condominio
        public MetodoCalculoMedia MetodoCalculoMedia { get; private set; }
        public FormatoRelatorio FormatoRelatorio { get; private set; }
        public PeriodicidadeAlerta PeriodicidadeAlerta { get; private set; }
        public decimal ValorMetroCubico { get; private set; }

        // Navigation property para Condominio (1-1)
        public Condominio Condominio { get; private set; }

        public ConfiguracaoCondominio()
        { }

        public ConfiguracaoCondominio(Guid condominioId, MetodoCalculoMedia metodoCalculoMedia, FormatoRelatorio formatoRelatorio, PeriodicidadeAlerta periodicidadeAlerta, decimal valorMetroCubico)
        {
            CondominioId = condominioId;
            MetodoCalculoMedia = metodoCalculoMedia;
            FormatoRelatorio = formatoRelatorio;
            PeriodicidadeAlerta = periodicidadeAlerta;
            ValorMetroCubico = valorMetroCubico;
        }

        //Update
        public void Update(MetodoCalculoMedia metodoCalculoMedia, FormatoRelatorio formatoRelatorio, PeriodicidadeAlerta periodicidadeAlerta, decimal valorMetroCubico)
        {
            MetodoCalculoMedia = metodoCalculoMedia;
            FormatoRelatorio = formatoRelatorio;
            PeriodicidadeAlerta = periodicidadeAlerta;
            ValorMetroCubico = valorMetroCubico;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}