using HydroMeasure.Hibrid.Shared.Model.Unidade;
using System.Net.Http.Json;

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

        public async Task<OperationResult<UnidadeDto>> CreateUnidadeAsync(CreateUnidadeCommand command)
        {
            var response = await _apiService.PostAsync(BaseEndpoint, command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<UnidadeDto>>();
                return result ?? new OperationResult<UnidadeDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<UnidadeDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<UnidadeDto>> UpdateUnidadeAsync(Guid id, UpdateUnidadeCommand command)
        {
            var response = await _apiService.PutAsync($"{BaseEndpoint}/{id}", command);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OperationResult<UnidadeDto>>();
                return result ?? new OperationResult<UnidadeDto> { Success = false, Message = "Falha ao processar resposta." };
            }

            return new OperationResult<UnidadeDto>
            {
                Success = false,
                Message = $"Erro: {response.ReasonPhrase}",
                ErrorCode = ((int)response.StatusCode).ToString()
            };
        }

        public async Task<OperationResult<bool>> DeleteUnidadeAsync(Guid id)
        {
            var response = await _apiService.DeleteAsync($"{BaseEndpoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return new OperationResult<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Unidade excluída com sucesso."
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