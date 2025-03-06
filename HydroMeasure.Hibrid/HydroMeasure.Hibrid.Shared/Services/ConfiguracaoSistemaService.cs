using HydroMeasure.Hibrid.Shared.Model.Configuracao;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class ConfiguracaoSistemaService : IConfiguracaoSistemaService
    {
        private readonly IApiService _apiService;
        private readonly ILogger<ConfiguracaoSistemaService> _logger;
        private const string BaseEndpoint = "api/ConfiguracaoSistema";

        // Tempo de expiração do cache para configurações do sistema (30 minutos)
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);

        public ConfiguracaoSistemaService(IApiService apiService, ILogger<ConfiguracaoSistemaService> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<ConfiguracaoSistemaDto?> GetConfiguracaoSistemaAsync()
        {
            try
            {
                _logger.LogInformation("Obtendo configurações do sistema");
                return await _apiService.GetAsync<ConfiguracaoSistemaDto>(BaseEndpoint, true, CacheExpiration);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Erro ao obter configurações do sistema: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao obter configurações do sistema: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<OperationResult<ConfiguracaoSistemaDto>> CreateConfiguracaoSistemaAsync(CreateConfiguracaoSistemaCommand command)
        {
            try
            {
                _logger.LogInformation("Criando configurações do sistema");
                var response = await _apiService.PostAsync(BaseEndpoint, command);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<ConfiguracaoSistemaDto>>();
                    if (result != null)
                    {
                        _logger.LogInformation("Configurações do sistema criadas com sucesso");
                        return result;
                    }

                    _logger.LogWarning("Falha ao processar resposta da criação de configurações");
                    return new OperationResult<ConfiguracaoSistemaDto> { Success = false, Message = "Falha ao processar resposta." };
                }

                _logger.LogWarning("Erro ao criar configurações do sistema: {StatusCode} - {Reason}",
                    (int)response.StatusCode, response.ReasonPhrase);

                return new OperationResult<ConfiguracaoSistemaDto>
                {
                    Success = false,
                    Message = $"Erro: {response.ReasonPhrase}",
                    ErrorCode = ((int)response.StatusCode).ToString()
                };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Erro de API ao criar configurações do sistema: {Message}", ex.Message);
                return new OperationResult<ConfiguracaoSistemaDto>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ((int)ex.StatusCode).ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar configurações do sistema: {Message}", ex.Message);
                return new OperationResult<ConfiguracaoSistemaDto>
                {
                    Success = false,
                    Message = $"Erro inesperado: {ex.Message}",
                    ErrorCode = "500"
                };
            }
        }

        public async Task<OperationResult<ConfiguracaoSistemaDto>> UpdateConfiguracaoSistemaAsync(UpdateConfiguracaoSistemaCommand command)
        {
            try
            {
                _logger.LogInformation("Atualizando configurações do sistema ID: {Id}", command.Id);
                var response = await _apiService.PutAsync($"{BaseEndpoint}/{command.Id}", command);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult<ConfiguracaoSistemaDto>>();
                    if (result != null)
                    {
                        _logger.LogInformation("Configurações do sistema atualizadas com sucesso");
                        return result;
                    }

                    _logger.LogWarning("Falha ao processar resposta da atualização de configurações");
                    return new OperationResult<ConfiguracaoSistemaDto> { Success = false, Message = "Falha ao processar resposta." };
                }

                _logger.LogWarning("Erro ao atualizar configurações do sistema: {StatusCode} - {Reason}",
                    (int)response.StatusCode, response.ReasonPhrase);

                return new OperationResult<ConfiguracaoSistemaDto>
                {
                    Success = false,
                    Message = $"Erro: {response.ReasonPhrase}",
                    ErrorCode = ((int)response.StatusCode).ToString()
                };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Erro de API ao atualizar configurações do sistema: {Message}", ex.Message);
                return new OperationResult<ConfiguracaoSistemaDto>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ((int)ex.StatusCode).ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar configurações do sistema: {Message}", ex.Message);
                return new OperationResult<ConfiguracaoSistemaDto>
                {
                    Success = false,
                    Message = $"Erro inesperado: {ex.Message}",
                    ErrorCode = "500"
                };
            }
        }

        public async Task<OperationResult<bool>> ResetConfiguracaoSistemaAsync()
        {
            try
            {
                _logger.LogInformation("Resetando configurações do sistema");
                var response = await _apiService.PostAsync<object>($"{BaseEndpoint}/reset", null);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Configurações do sistema resetadas com sucesso");
                    return new OperationResult<bool>
                    {
                        Success = true,
                        Data = true,
                        Message = "Configurações de sistema redefinidas com sucesso."
                    };
                }

                _logger.LogWarning("Erro ao resetar configurações do sistema: {StatusCode} - {Reason}",
                    (int)response.StatusCode, response.ReasonPhrase);

                return new OperationResult<bool>
                {
                    Success = false,
                    Data = false,
                    Message = $"Erro: {response.ReasonPhrase}",
                    ErrorCode = ((int)response.StatusCode).ToString()
                };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Erro de API ao resetar configurações do sistema: {Message}", ex.Message);
                return new OperationResult<bool>
                {
                    Success = false,
                    Data = false,
                    Message = ex.Message,
                    ErrorCode = ((int)ex.StatusCode).ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao resetar configurações do sistema: {Message}", ex.Message);
                return new OperationResult<bool>
                {
                    Success = false,
                    Data = false,
                    Message = $"Erro inesperado: {ex.Message}",
                    ErrorCode = "500"
                };
            }
        }
    }
}