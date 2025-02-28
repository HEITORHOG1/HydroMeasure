namespace HydroMeasure.Hibrid.Shared.Model.Condominio
{
    public class CreateCondominioCommand
    {
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string? CNPJ { get; set; }
        public string? Sindico { get; set; }
        public string? TelefoneSindico { get; set; }
        public string? EmailSindico { get; set; }
    }
}