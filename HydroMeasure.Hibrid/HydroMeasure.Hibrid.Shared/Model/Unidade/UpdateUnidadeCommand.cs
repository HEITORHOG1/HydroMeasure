namespace HydroMeasure.Hibrid.Shared.Model.Unidade
{
    public class UpdateUnidadeCommand
    {
        public Guid Id { get; set; }
        public Guid CondominioId { get; set; }
        public string Identificacao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string? MoradorResponsavel { get; set; }
        public decimal MediaConsumo { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
