namespace HydroMeasure.Hibrid.Shared.Model.Condominio
{
    public class CondominioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CNPJ { get; set; }
        public string Sindico { get; set; }
        public string TelefoneSindico { get; set; }
        public string EmailSindico { get; set; }
        public bool Ativo { get; set; }

        public CondominioDto()
        { }

        public CondominioDto(Guid id, string nome, string endereco, string cnpj, string sindico, string telefoneSindico, string emailSindico, bool ativo)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            CNPJ = cnpj;
            Sindico = sindico;
            TelefoneSindico = telefoneSindico;
            EmailSindico = emailSindico;
            Ativo = ativo;
        }
    }
}