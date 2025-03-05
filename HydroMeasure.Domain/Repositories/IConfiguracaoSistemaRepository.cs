using HydroMeasure.Domain.Entities;

namespace HydroMeasure.Domain.Repositories
{
    public interface IConfiguracaoSistemaRepository
    {
        Task<ConfiguracaoSistema?> GetAsync();

        Task<ConfiguracaoSistema?> GetByIdAsync(Guid id);

        Task<ConfiguracaoSistema> AddAsync(ConfiguracaoSistema configuracao);

        Task<ConfiguracaoSistema> UpdateAsync(ConfiguracaoSistema configuracao);

        Task<bool> DeleteAsync(Guid id);
    }
}