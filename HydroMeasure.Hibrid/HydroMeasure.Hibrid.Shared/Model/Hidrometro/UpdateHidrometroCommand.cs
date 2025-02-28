namespace HydroMeasure.Hibrid.Shared.Model.Hidrometro
{
    public class UpdateHidrometroCommand
    {
        public Guid Id { get; set; }
        public Guid UnidadeId { get; set; }
        public string NumeroSerie { get; set; } = string.Empty;
        public string? Modelo { get; set; }
        public DateTime? DataInstalacao { get; set; }
        public bool Ativo { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
