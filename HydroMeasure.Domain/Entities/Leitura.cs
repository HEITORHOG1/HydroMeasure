using HydroMeasure.Domain.Entities.Base;

namespace HydroMeasure.Domain.Entities
{
    public class Leitura : EntityBase
    {
        public Guid HidrometroId { get; private set; }
        public Guid UnidadeId { get; private set; }
        public decimal LeituraAtual { get; private set; }
        public DateTime DataLeitura { get; private set; }
        public decimal Consumo { get; private set; }
        public Guid? LeituraAnteriorId { get; private set; } // Nullable

        // Navigation Properties
        public Hidrometro Hidrometro { get; private set; }

        public Unidade Unidade { get; private set; }
        public Leitura? LeituraAnterior { get; private set; } // Nullable

        public Leitura()
        {
            // Construtor vazio para EF Core
        }

        // **Adicione este construtor:**
        public Leitura(Guid hidrometroId, Guid unidadeId, decimal leituraAtual, DateTime dataLeitura, decimal consumo, Guid? leituraAnteriorId)
        {
            HidrometroId = hidrometroId;
            UnidadeId = unidadeId;
            LeituraAtual = leituraAtual;
            DataLeitura = dataLeitura;
            Consumo = consumo;
            LeituraAnteriorId = leituraAnteriorId;
            // Validações de domínio podem ser adicionadas aqui se necessário
        }

        // **Adicione este método para atualizar a leitura:**
        public void Update(decimal leituraAtual, DateTime dataLeitura, decimal consumo, Guid? leituraAnteriorId)
        {
            LeituraAtual = leituraAtual;
            DataLeitura = dataLeitura;
            Consumo = consumo;
            LeituraAnteriorId = leituraAnteriorId;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}