using HydroMeasure.Hibrid.Shared.Model.Hidrometro;
using System.Net.Http.Json;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class HidrometroService : IHidrometroService
    {
        private readonly IApiService _apiService;
        private const string BaseEndpoint = "api/hidrometro";

        public HidrometroService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<HidrometroDto>> GetAllHidrometrosAsync()
        {
            var hidrometros = await _apiService.GetListAsync<HidrometroDto>(BaseEndpoint);
            return hidrometros ?? new List<HidrometroDto>();
        }

        public async Task<List<HidrometroDto>> GetHidrometrosByUnidadeAsync(Guid unidadeId)
        {
            // Usando um endpoint específico (se existir) ou filtrando no cliente
            var hidrometros = await _apiService.GetListAsync<HidrometroDto>($"{BaseEndpoint}?unidadeId={unidadeId}");
            return hidrometros?.Where(h => h.UnidadeId == unidadeId).ToList() ?? new List<HidrometroDto>();
        }

        public async Task<HidrometroDto?> GetHidrometroByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<HidrometroDto>($"{BaseEndpoint}/{id}");
        }

        public async Task<OperationResult<HidrometroDto>> CreateHidrometroAsync(CreateHidrometroCommand command)
        {
            var response = await _apiService.PostAsync(BaseEndpoint, command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<HidrometroDto>>();
                return result ?? new OperationResult<HidrometroDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<HidrometroDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<HidrometroDto>> UpdateHidrometroAsync(Guid id, UpdateHidrometroCommand command)
        {
            var response = await _apiService.PutAsync($"{BaseEndpoint}/{id}", command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<HidrometroDto>>();
                return result ?? new OperationResult<HidrometroDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<HidrometroDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<bool>> DeleteHidrometroAsync(Guid id)
        {
            var response = await _apiService.DeleteAsync($"{BaseEndpoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return new OperationResult<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Hidrômetro excluído com sucesso."
                };
            }

            return new OperationResult<bool>
            {
                Success = false,
                Data = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }
    }
}