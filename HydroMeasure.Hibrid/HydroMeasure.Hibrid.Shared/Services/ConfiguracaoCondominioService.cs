using HydroMeasure.Hibrid.Shared.Model.ConfiguracaoCondominio;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class ConfiguracaoCondominioService : IConfiguracaoCondominioService
    {
        private readonly IApiService _apiService;
        private const string BaseEndpoint = "api/configuracaocondominio";

        public ConfiguracaoCondominioService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ConfiguracaoCondominioDto?> GetByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<ConfiguracaoCondominioDto>($"{BaseEndpoint}/{id}");
        }

        public async Task<ConfiguracaoCondominioDto?> GetByCondominioIdAsync(Guid condominioId)
        {
            // Assumindo que existe um endpoint para buscar por condominioId
            // Se não existir, precisamos criar um método no controller da API ou buscar todos e filtrar
            var configs = await _apiService.GetListAsync<ConfiguracaoCondominioDto>($"{BaseEndpoint}");
            return configs?.FirstOrDefault(c => c.CondominioId == condominioId);
        }

        public async Task<List<ConfiguracaoCondominioDto>> GetAllAsync()
        {
            var configs = await _apiService.GetListAsync<ConfiguracaoCondominioDto>(BaseEndpoint);
            return configs ?? new List<ConfiguracaoCondominioDto>();
        }
    }
}