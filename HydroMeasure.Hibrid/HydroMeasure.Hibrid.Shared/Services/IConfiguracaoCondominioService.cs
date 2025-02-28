using HydroMeasure.Hibrid.Shared.Model.ConfiguracaoCondominio;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface IConfiguracaoCondominioService
    {
        Task<ConfiguracaoCondominioDto?> GetByIdAsync(Guid id);

        Task<ConfiguracaoCondominioDto?> GetByCondominioIdAsync(Guid condominioId);

        Task<List<ConfiguracaoCondominioDto>> GetAllAsync();
    }
}