using HydroMeasure.Domain.Entities.Base;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Domain.Entities
{
    public class Hidrometro : EntityBase // Herda de EntityBase
    {
        public Guid UnidadeId { get; private set; } // FK para Unidade
        public string NumeroSerie { get; private set; }
        public string? Modelo { get; private set; }
        public DateTime? DataInstalacao { get; private set; }
        public bool Ativo { get; set; }
        public StatusHidrometro Status { get; private set; } // Novo enum StatusHidrometro

        // Navigation property para Unidade (1-N)
        public Unidade Unidade { get; private set; }

        // Navigation property para Leituras (1-N)
        public List<Leitura> Leituras { get; private set; }

        public Hidrometro()
        { }

        public Hidrometro(Guid unidadeId, string numeroSerie, string? modelo, DateTime? dataInstalacao)
        {
            UnidadeId = unidadeId;
            NumeroSerie = numeroSerie;
            Modelo = modelo;
            DataInstalacao = dataInstalacao;
            Ativo = true;
            Status = StatusHidrometro.Ativo;
        }

        //Update
        public void Update(Guid unidadeId, string numeroSerie, string? modelo, DateTime? dataInstalacao, bool ativo, StatusHidrometro status)
        {
            UnidadeId = unidadeId;
            NumeroSerie = numeroSerie;
            Modelo = modelo;
            DataInstalacao = dataInstalacao;
            Ativo = ativo;
            Status = status;
            DataAlteracao = DateTime.Now;
        }
    }
}