using HydroMeasure.Hibrid.Shared.Model.Unidade;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class UnidadeService : IUnidadeService
    {
        private readonly IApiService _apiService;
        private const string BaseEndpoint = "api/unidade";

        public UnidadeService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<UnidadeDto>> GetAllUnidadesAsync()
        {
            var unidades = await _apiService.GetListAsync<UnidadeDto>(BaseEndpoint);
            return unidades ?? new List<UnidadeDto>();
        }

        public async Task<List<UnidadeDto>> GetUnidadesByCondominioAsync(Guid condominioId)
        {
            // Assumindo que existe um endpoint para buscar por condomínio
            // Se não existir, precisamos criar um método no controller da API
            var unidades = await _apiService.GetListAsync<UnidadeDto>($"{BaseEndpoint}/condominio/{condominioId}");
            return unidades ?? new List<UnidadeDto>();
        }

        public async Task<UnidadeDto?> GetUnidadeByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<UnidadeDto>($"{BaseEndpoint}/{id}");
        }
    }
}