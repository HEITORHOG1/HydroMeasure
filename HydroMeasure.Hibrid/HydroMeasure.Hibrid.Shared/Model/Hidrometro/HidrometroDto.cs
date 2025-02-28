namespace HydroMeasure.Hibrid.Shared.Model.Hidrometro
{
    public class HidrometroDto
    {
        public Guid Id { get; set; }
        public Guid UnidadeId { get; set; }
        public string NumeroSerie { get; set; } = string.Empty;
        public string? Modelo { get; set; }
        public DateTime? DataInstalacao { get; set; }
        public bool Ativo { get; set; }
        public string Status { get; set; } = string.Empty;

        // Propriedades de navegação (opcional - para exibição)
        public string IdentificacaoUnidade { get; set; } = string.Empty;
    }
}