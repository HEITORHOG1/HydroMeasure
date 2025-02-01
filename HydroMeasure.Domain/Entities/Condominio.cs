using HydroMeasure.Domain.Entities.Base;

namespace HydroMeasure.Domain.Entities
{
    public class Condominio : EntityBase
    {
        public string Nome { get; private set; }
        public string Endereco { get; private set; }
        public string? CNPJ { get; private set; }
        public string? Sindico { get; private set; }
        public string? TelefoneSindico { get; private set; }
        public string? EmailSindico { get; private set; }
        public bool Ativo { get; private set; }

        // Navigation property para ConfiguracaoCondominio (1-1)
        public ConfiguracaoCondominio Configuracao { get; private set; }

        // Navigation property para Unidades (1-N)
        public List<Unidade> Unidades { get; private set; }

        // Navigation property para Usuarios (1-N)
        public List<Usuario> Usuarios { get; private set; }

        // Navigation property para Alertas (1-N)
        public List<Alerta> Alertas { get; private set; }

        public Condominio()
        {
        }

        public Condominio(string nome, string endereco, string cnpj, string sindico, string telefoneSindico, string emailSindico)
        {
            Nome = nome;
            Endereco = endereco;
            CNPJ = cnpj;
            Sindico = sindico;
            TelefoneSindico = telefoneSindico;
            EmailSindico = emailSindico;
            Ativo = true;
        }

        //Update
        public void Update(string nome, string endereco, string cnpj, string sindico, string telefoneSindico, string emailSindico, bool ativo)
        {
            Nome = nome;
            Endereco = endereco;
            CNPJ = cnpj;
            Sindico = sindico;
            TelefoneSindico = telefoneSindico;
            EmailSindico = emailSindico;
            Ativo = ativo;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}