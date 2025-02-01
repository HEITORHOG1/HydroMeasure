using HydroMeasure.Domain.Entities.Base;

namespace HydroMeasure.Domain.Entities
{
    public class Leitura : EntityBase
    {
        public Guid HidrometroId { get; private set; } // FK para Hidrometro
        public decimal LeituraAtual { get; private set; }
        public DateTime DataLeitura { get; private set; }
        public decimal Consumo { get; private set; }
        public Guid? LeituraAnteriorId { get; private set; } // FK auto-referenciada para Leitura (Leitura Anterior)

        // Navigation property para Hidrometro (1-N)
        public Hidrometro Hidrometro { get; private set; }

        // Navigation property auto-referenciada para LeituraAnterior (0..1 - 1)
        public Leitura? LeituraAnterior { get; private set; } // Permite ser nulo (Leitura inicial não tem anterior)

        // Nova propriedade de navegação para Unidade (N-1) - Leitura pertence a uma Unidade
        public Guid UnidadeId { get; set; } // Chave estrangeira para Unidade

        public Unidade Unidade { get; set; } // Propriedade de navegação

        public Leitura()
        { }

        public Leitura(Guid hidrometroId, decimal leituraAtual, DateTime dataLeitura, decimal consumo)
        {
            HidrometroId = hidrometroId;
            LeituraAtual = leituraAtual;
            DataLeitura = dataLeitura;
            Consumo = consumo;
        }

        //Update
        public void Update(Guid hidrometroId, decimal leituraAtual, DateTime dataLeitura, decimal consumo, Guid? leituraAnteriorId)
        {
            HidrometroId = hidrometroId;
            LeituraAtual = leituraAtual;
            DataLeitura = dataLeitura;
            Consumo = consumo;
            LeituraAnteriorId = leituraAnteriorId;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}