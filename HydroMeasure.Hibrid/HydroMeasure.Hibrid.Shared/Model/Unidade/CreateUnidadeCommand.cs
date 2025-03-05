namespace HydroMeasure.Hibrid.Shared.Model.Unidade
{
    public class CreateUnidadeCommand
    {
        public Guid CondominioId { get; set; }
        public string Identificacao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // TipoUnidade como string
        public string? MoradorResponsavel { get; set; }
        public decimal MediaConsumo { get; set; }
    }
}