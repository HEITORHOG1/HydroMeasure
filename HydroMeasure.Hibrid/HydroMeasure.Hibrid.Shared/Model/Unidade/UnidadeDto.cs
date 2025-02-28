namespace HydroMeasure.Hibrid.Shared.Model.Unidade
{
    public class UnidadeDto
    {
        public Guid Id { get; set; }
        public Guid CondominioId { get; set; }
        public string Identificacao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string? MoradorResponsavel { get; set; }
        public decimal MediaConsumo { get; set; }
        public bool Ativo { get; set; }
        public string Status { get; set; } = string.Empty;

        public UnidadeDto(Guid id, Guid condominioId, string identificacao, string tipo, string? moradorResponsavel, decimal mediaConsumo, bool ativo, string status)
        {
            Id = id;
            CondominioId = condominioId;
            Identificacao = identificacao;
            Tipo = tipo;
            MoradorResponsavel = moradorResponsavel;
            MediaConsumo = mediaConsumo;
            Ativo = ativo;
            Status = status;
        }
    }
}
