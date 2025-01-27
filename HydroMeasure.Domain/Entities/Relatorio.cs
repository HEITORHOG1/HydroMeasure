using HydroMeasure.Domain.Entities.Base;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Domain.Entities
{
    public class Relatorio : EntityBase // Herda de EntityBase
    {
        public TipoRelatorio Tipo { get; private set; }
        public string Periodo { get; private set; }
        public string Conteudo { get; private set; } // JSON ou XML (ou outro formato)
        public Guid UsuarioId { get; private set; } // FK para Usuario

        // Navigation property para Usuario (1-N) - Usuário que gerou o relatório
        public Usuario Usuario { get; private set; }

        public Relatorio()
        { }

        public Relatorio(TipoRelatorio tipo, string periodo, string conteudo, Guid usuarioId)
        {
            Tipo = tipo;
            Periodo = periodo;
            Conteudo = conteudo;
            UsuarioId = usuarioId;
        }

        //Update
        public void Update(TipoRelatorio tipo, string periodo, string conteudo, Guid usuarioId)
        {
            Tipo = tipo;
            Periodo = periodo;
            Conteudo = conteudo;
            UsuarioId = usuarioId;
            DataAlteracao = DateTime.Now;
        }
    }
}