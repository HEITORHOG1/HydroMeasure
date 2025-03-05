namespace HydroMeasure.Hibrid.Shared.Model.Leitura
{
    public class UpdateLeituraCommand
    {
        public Guid Id { get; set; }
        public Guid HidrometroId { get; set; }
        public Guid UnidadeId { get; set; }
        public decimal LeituraAtual { get; set; }
        public DateTime DataLeitura { get; set; }
        public decimal Consumo { get; set; }
        public Guid? LeituraAnteriorId { get; set; }
    }
}