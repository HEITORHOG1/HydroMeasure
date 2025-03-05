namespace HydroMeasure.Hibrid.Shared.Model.Leitura
{
    public class LeituraDto
    {
        public Guid Id { get; set; }
        public Guid HidrometroId { get; set; }
        public Guid UnidadeId { get; set; }
        public string NumeroSerieHidrometro { get; set; } = string.Empty;
        public string IdentificacaoUnidade { get; set; } = string.Empty;
        public decimal LeituraAtual { get; set; }
        public DateTime DataLeitura { get; set; }
        public decimal Consumo { get; set; }
        public Guid? LeituraAnteriorId { get; set; }
    }
}