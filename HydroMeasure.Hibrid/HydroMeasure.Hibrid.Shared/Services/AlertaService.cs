namespace HydroMeasure.Hibrid.Shared.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly IApiService _apiService;
        private const string BaseEndpoint = "api/alerta";

        public AlertaService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<AlertaDto>> GetAlertasByCondominioAsync(Guid condominioId)
        {
            var alertas = await _apiService.GetListAsync<AlertaDto>($"{BaseEndpoint}/condominio/{condominioId}");
            return alertas ?? new List<AlertaDto>();
        }

        public async Task<List<AlertaDto>> GetAlertasByUnidadeAsync(Guid unidadeId)
        {
            var alertas = await _apiService.GetListAsync<AlertaDto>($"{BaseEndpoint}/unidade/{unidadeId}");
            return alertas ?? new List<AlertaDto>();
        }

        public async Task<List<AlertaDto>> GetAlertasNaoResolvidosAsync(Guid condominioId)
        {
            var alertas = await _apiService.GetListAsync<AlertaDto>($"{BaseEndpoint}/condominio/{condominioId}/nao-resolvidos");
            return alertas ?? new List<AlertaDto>();
        }

        public async Task<AlertaDto?> GetAlertaByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<AlertaDto>($"{BaseEndpoint}/{id}");
        }
    }
}