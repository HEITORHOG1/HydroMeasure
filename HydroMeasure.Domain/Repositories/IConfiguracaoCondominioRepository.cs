using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories.Base;

namespace HydroMeasure.Domain.Repositories
{
    public interface IConfiguracaoCondominioRepository : IRepository<ConfiguracaoCondominio>
    {
        // Métodos específicos para ConfiguracaoCondominio podem ser adicionados aqui no futuro, se necessário.
        // Por enquanto, podemos deixar a interface básica herdando de IRepository<ConfiguracaoCondominio>.
    }
}