namespace HydroMeasure.Hibrid.Shared.Model.Leitura
{
    public class CreateLeituraCommand
    {
        public Guid HidrometroId { get; set; }
        public Guid UnidadeId { get; set; }
        public decimal LeituraAtual { get; set; }
        public DateTime DataLeitura { get; set; } = DateTime.Now;
        public Guid? LeituraAnteriorId { get; set; }
    }
}