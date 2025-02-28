using HydroMeasure.Hibrid.Shared.Model.Configuracao;
using System.Net.Http.Json;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class ConfiguracaoSistemaService : IConfiguracaoSistemaService
    {
        private readonly IApiService _apiService;
        private const string BaseEndpoint = "api/configuracaosistema";

        public ConfiguracaoSistemaService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ConfiguracaoSistemaDto?> GetConfiguracaoSistemaAsync()
        {
            return await _apiService.GetAsync<ConfiguracaoSistemaDto>(BaseEndpoint);
        }

        public async Task<OperationResult<ConfiguracaoSistemaDto>> CreateConfiguracaoSistemaAsync(CreateConfiguracaoSistemaCommand command)
        {
            var response = await _apiService.PostAsync(BaseEndpoint, command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<ConfiguracaoSistemaDto>>();
                return result ?? new OperationResult<ConfiguracaoSistemaDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<ConfiguracaoSistemaDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<ConfiguracaoSistemaDto>> UpdateConfiguracaoSistemaAsync(UpdateConfiguracaoSistemaCommand command)
        {
            var response = await _apiService.PutAsync($"{BaseEndpoint}/{command.Id}", command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<ConfiguracaoSistemaDto>>();
                return result ?? new OperationResult<ConfiguracaoSistemaDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<ConfiguracaoSistemaDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<bool>> ResetConfiguracaoSistemaAsync()
        {
            var response = await _apiService.PostAsync<object>($"{BaseEndpoint}/reset", null);
            if (response.IsSuccessStatusCode)
            {
                return new OperationResult<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Configurações de sistema redefinidas com sucesso."
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