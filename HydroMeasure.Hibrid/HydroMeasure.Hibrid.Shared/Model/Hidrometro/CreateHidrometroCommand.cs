namespace HydroMeasure.Hibrid.Shared.Model.Hidrometro
{
    public class CreateHidrometroCommand
    {
        public Guid UnidadeId { get; set; }
        public string NumeroSerie { get; set; } = string.Empty;
        public string? Modelo { get; set; }
        public DateTime? DataInstalacao { get; set; }
    }
}