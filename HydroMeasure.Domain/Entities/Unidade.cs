using HydroMeasure.Domain.Entities.Base;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Domain.Entities
{
    public class Unidade : EntityBase // Herda de EntityBase
    {
        public Guid CondominioId { get; private set; } // FK para Condominio
        public string Identificacao { get; private set; }
        public TipoUnidade Tipo { get; private set; }
        public string? MoradorResponsavel { get; private set; }
        public decimal MediaConsumo { get; private set; }

        public bool Ativo { get; private set; }
        public StatusUnidade Status { get; private set; } // Novo enum StatusUnidade

        // Navigation property para Condominio (1-N)
        public Condominio Condominio { get; private set; }

        // Navigation property para Hidrometros (1-N)
        public List<Hidrometro> Hidrometros { get; private set; }

        // Navigation property para Leituras (1-N)
        public List<Leitura> Leituras { get; private set; }

        // Navigation property para Alertas (1-N)
        public List<Alerta> Alertas { get; private set; }

        public Unidade()
        { }

        public Unidade(Guid condominioId, string identificacao, TipoUnidade tipo, string moradorResponsavel, decimal mediaConsumo)
        {
            CondominioId = condominioId;
            Identificacao = identificacao;
            Tipo = tipo;
            MoradorResponsavel = moradorResponsavel;
            MediaConsumo = mediaConsumo;
            Ativo = true;
            Status = StatusUnidade.Ativo;
        }

        //Update

        public void Update(Guid condominioId, string identificacao, TipoUnidade tipo, string moradorResponsavel, decimal mediaConsumo, bool ativo, StatusUnidade status)
        {
            CondominioId = condominioId;
            Identificacao = identificacao;
            Tipo = tipo;
            MoradorResponsavel = moradorResponsavel;
            MediaConsumo = mediaConsumo;
            Ativo = ativo;
            Status = status;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}