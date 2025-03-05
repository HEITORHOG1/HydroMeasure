using HydroMeasure.Hibrid.Shared.Model.Leitura;
using System.Net.Http.Json;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class LeituraService : ILeituraService
    {
        private readonly IApiService _apiService;
        private const string BaseEndpoint = "api/leitura";

        public LeituraService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<LeituraDto>> GetAllLeiturasAsync()
        {
            var leituras = await _apiService.GetListAsync<LeituraDto>(BaseEndpoint);
            return leituras ?? new List<LeituraDto>();
        }

        public async Task<List<LeituraDto>> GetLeiturasByHidrometroAsync(Guid hidrometroId)
        {
            var leituras = await _apiService.GetListAsync<LeituraDto>($"{BaseEndpoint}/hidrometros/{hidrometroId}");
            return leituras ?? new List<LeituraDto>();
        }

        public async Task<List<LeituraDto>> GetLeiturasByUnidadeAsync(Guid unidadeId)
        {
            var leituras = await _apiService.GetListAsync<LeituraDto>($"{BaseEndpoint}/unidades/{unidadeId}");
            return leituras ?? new List<LeituraDto>();
        }

        public async Task<LeituraDto?> GetLeituraByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<LeituraDto>($"{BaseEndpoint}/{id}");
        }

        public async Task<LeituraDto?> GetUltimaLeituraByHidrometroAsync(Guid hidrometroId)
        {
            var leituras = await GetLeiturasByHidrometroAsync(hidrometroId);
            return leituras.OrderByDescending(l => l.DataLeitura).FirstOrDefault();
        }

        public async Task<OperationResult<LeituraDto>> CreateLeituraAsync(CreateLeituraCommand command)
        {
            var response = await _apiService.PostAsync(BaseEndpoint, command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<LeituraDto>>();
                return result ?? new OperationResult<LeituraDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<LeituraDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<LeituraDto>> UpdateLeituraAsync(Guid id, UpdateLeituraCommand command)
        {
            var response = await _apiService.PutAsync($"{BaseEndpoint}/{id}", command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<LeituraDto>>();
                return result ?? new OperationResult<LeituraDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<LeituraDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<bool>> DeleteLeituraAsync(Guid id)
        {
            var response = await _apiService.DeleteAsync($"{BaseEndpoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return new OperationResult<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Leitura excluída com sucesso."
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