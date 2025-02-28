using HydroMeasure.Hibrid.Shared.Model.Condominio;
using System.Net.Http.Json;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class CondominioService : ICondominioService
    {
        private readonly IApiService _apiService;
        private const string BaseEndpoint = "api/condominio";

        public CondominioService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<CondominioDto>> GetAllCondominiosAsync()
        {
            var condominios = await _apiService.GetListAsync<CondominioDto>(BaseEndpoint);
            return condominios ?? new List<CondominioDto>();
        }

        public async Task<CondominioDto?> GetCondominioByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<CondominioDto>($"{BaseEndpoint}/{id}");
        }

        public async Task<OperationResult<CondominioDto>> CreateCondominioAsync(CreateCondominioCommand command)
        {
            var response = await _apiService.PostAsync(BaseEndpoint, command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<CondominioDto>>();
                return result ?? new OperationResult<CondominioDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<CondominioDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<CondominioDto>> UpdateCondominioAsync(Guid id, UpdateCondominioCommand command)
        {
            var response = await _apiService.PutAsync($"{BaseEndpoint}/{id}", command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<CondominioDto>>();
                return result ?? new OperationResult<CondominioDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<CondominioDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<bool>> DeleteCondominioAsync(Guid id)
        {
            var response = await _apiService.DeleteAsync($"{BaseEndpoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return new OperationResult<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Condomínio excluído com sucesso."
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