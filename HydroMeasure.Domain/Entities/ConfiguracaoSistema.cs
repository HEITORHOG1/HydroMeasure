using HydroMeasure.Domain.Entities.Base;

namespace HydroMeasure.Domain.Entities
{
    public class ConfiguracaoSistema : EntityBase // Herda de EntityBase
    {
        public string NomeSistema { get; private set; }
        public string VersaoSistema { get; private set; }

        public ConfiguracaoSistema()
        { }

        public ConfiguracaoSistema(string nomeSistema, string versaoSistema)
        {
            NomeSistema = nomeSistema;
            VersaoSistema = versaoSistema;
        }
    }
}