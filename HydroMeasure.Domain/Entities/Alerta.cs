using HydroMeasure.Domain.Entities.Base;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Domain.Entities
{
    public class Alerta : EntityBase
    {
        public Guid UnidadeId { get; private set; } // FK para Unidade
        public TipoAlerta Tipo { get; private set; }
        public string Mensagem { get; private set; }
        public DateTime DataAlerta { get; private set; }
        public bool Resolvido { get; private set; }
        public Guid? UsuarioResolvidoId { get; private set; } // FK para Usuario (opcional - usuário que resolveu)

        // Navigation property para Unidade (1-N)
        public Unidade Unidade { get; private set; }

        // Navigation property para Usuario (0..1 - 1) - Usuário que resolveu o alerta (opcional)
        public Usuario? UsuarioResolvido { get; private set; } // Permite ser nulo (alerta pode não ter sido resolvido ainda)

        // Nova propriedade de navegação para Condominio (N-1) - Alerta pertence a um Condominio
        public Guid CondominioId { get; set; } // Chave estrangeira para Condominio

        public Condominio Condominio { get; set; } // Propriedade de navegação

        public Alerta()
        { }

        public Alerta(Guid unidadeId, TipoAlerta tipo, string mensagem, DateTime dataAlerta)
        {
            UnidadeId = unidadeId;
            Tipo = tipo;
            Mensagem = mensagem;
            DataAlerta = dataAlerta;
            Resolvido = false;
        }

        //Update
        public void Update(Guid unidadeId, TipoAlerta tipo, string mensagem, DateTime dataAlerta, bool resolvido, Guid? usuarioResolvidoId)
        {
            UnidadeId = unidadeId;
            Tipo = tipo;
            Mensagem = mensagem;
            DataAlerta = dataAlerta;
            Resolvido = resolvido;
            UsuarioResolvidoId = usuarioResolvidoId;
            DataAlteracao = DateTime.Now;
        }
    }
}