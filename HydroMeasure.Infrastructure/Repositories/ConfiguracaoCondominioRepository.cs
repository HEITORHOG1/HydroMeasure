using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories.Base;

namespace HydroMeasure.Infrastructure.Repositories
{
    public class ConfiguracaoCondominioRepository : BaseRepository<ConfiguracaoCondominio>, IConfiguracaoCondominioRepository
    {
        public ConfiguracaoCondominioRepository(HydroMeasureDbContext context) : base(context)
        {
        }

        // Métodos específicos de ConfiguracaoCondominioRepository (se necessário) podem ser adicionados aqui no futuro.
        // Por enquanto, a implementação padrão de BaseRepository<ConfiguracaoCondominio> e IConfiguracaoCondominioRepository é suficiente.
    }
}