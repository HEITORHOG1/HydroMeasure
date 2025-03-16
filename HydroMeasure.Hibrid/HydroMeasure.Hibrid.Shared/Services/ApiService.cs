using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Collections.Concurrent;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;
        private static readonly ConcurrentDictionary<string, CacheEntry> _cache = new();

        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T?> GetAsync<T>(string endpoint, bool useCache = true, TimeSpan? cacheExpiration = null)
        {
            string cacheKey = $"GET_{endpoint}";
            
            // Verificar cache se habilitado
            if (useCache && TryGetFromCache<T>(cacheKey, out var cachedResult))
            {
                _logger.LogInformation("Cache hit for {Endpoint}", endpoint);
                return cachedResult;
            }

            try
            {
                _logger.LogInformation("Executing GET request to {Endpoint}", endpoint);
                
                var response = await _httpClient.GetAsync(endpoint);
                
                if (!response.IsSuccessStatusCode)
                {
                    await LogApiError(response, endpoint, "GET");
                    return default;
                }

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Resposta recebida de {Endpoint}: {Content}", endpoint, content);
                
                try {
                    var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);

                    // Armazenar em cache se habilitado
                    if (useCache && result != null)
                    {
                        var expiration = cacheExpiration ?? TimeSpan.FromMinutes(5);
                        AddToCache(cacheKey, result, expiration);
                        _logger.LogDebug("Cached result for {Endpoint} with expiration {Expiration}", endpoint, expiration);
                    }

                    return result;
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "JSON deserialization error in GET {Endpoint}: {Message}. Content: {Content}", endpoint, ex.Message, content);
                    throw new ApiException("Erro ao processar resposta da API: " + ex.Message, ex, HttpStatusCode.UnprocessableEntity);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network error in GET {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException($"Erro de conexão: {ex.Message}", ex, HttpStatusCode.ServiceUnavailable);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error in GET {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException("Erro ao processar resposta da API: " + ex.Message, ex, HttpStatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in GET {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException($"Erro inesperado: {ex.Message}", ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<T>?> GetListAsync<T>(string endpoint, bool useCache = true, TimeSpan? cacheExpiration = null)
        {
            string cacheKey = $"GET_LIST_{endpoint}";
            
            // Verificar cache se habilitado
            if (useCache && TryGetFromCache<List<T>>(cacheKey, out var cachedResult))
            {
                _logger.LogInformation("Cache hit for list {Endpoint}", endpoint);
                return cachedResult;
            }

            try
            {
                _logger.LogInformation("Executing GET list request to {Endpoint}", endpoint);
                
                var response = await _httpClient.GetAsync(endpoint);
                
                if (!response.IsSuccessStatusCode)
                {
                    await LogApiError(response, endpoint, "GET_LIST");
                    return new List<T>();
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<T>>(content, _jsonOptions);

                // Armazenar em cache se habilitado
                if (useCache && result != null)
                {
                    var expiration = cacheExpiration ?? TimeSpan.FromMinutes(5);
                    AddToCache(cacheKey, result, expiration);
                    _logger.LogDebug("Cached list result for {Endpoint} with expiration {Expiration}", endpoint, expiration);
                }

                return result ?? new List<T>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network error in GET list {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException($"Erro de conexão: {ex.Message}", ex, HttpStatusCode.ServiceUnavailable);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error in GET list {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException("Erro ao processar resposta da API", ex, HttpStatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in GET list {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException($"Erro inesperado: {ex.Message}", ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                _logger.LogInformation("Executing POST request to {Endpoint}", endpoint);
                
                var response = await _httpClient.PostAsJsonAsync(endpoint, data, _jsonOptions);
                
                if (!response.IsSuccessStatusCode)
                {
                    await LogApiError(response, endpoint, "POST");
                }
                
                // Invalidar cache relacionado
                InvalidateRelatedCache(endpoint);
                
                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network error in POST {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException($"Erro de conexão: {ex.Message}", ex, HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in POST {Endpoint}: {Message}", endpoint, ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
        {
            try
            {
                _logger.LogInformation("Executing PUT request to {Endpoint}", endpoint);
                
                var response = await _httpClient.PutAsJsonAsync(endpoint, data, _jsonOptions);
                
                if (!response.IsSuccessStatusCode)
                {
                    await LogApiError(response, endpoint, "PUT");
                }
                
                // Invalidar cache relacionado
                InvalidateRelatedCache(endpoint);
                
                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network error in PUT {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException($"Erro de conexão: {ex.Message}", ex, HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in PUT {Endpoint}: {Message}", endpoint, ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            try
            {
                _logger.LogInformation("Executing DELETE request to {Endpoint}", endpoint);
                
                var response = await _httpClient.DeleteAsync(endpoint);
                
                if (!response.IsSuccessStatusCode)
                {
                    await LogApiError(response, endpoint, "DELETE");
                }
                
                // Invalidar cache relacionado
                InvalidateRelatedCache(endpoint);
                
                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network error in DELETE {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException($"Erro de conexão: {ex.Message}", ex, HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in DELETE {Endpoint}: {Message}", endpoint, ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
            }
        }

        private async Task LogApiError(HttpResponseMessage response, string endpoint, string method)
        {
            string errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogWarning("API error in {Method} {Endpoint}: Status {StatusCode}, Content: {ErrorContent}", 
                method, endpoint, (int)response.StatusCode, errorContent);
        }

        private void InvalidateRelatedCache(string endpoint)
        {
            // Extrair a parte base do endpoint (sem ID)
            string baseEndpoint = endpoint.Split('/')[0];
            
            // Buscar e remover todas as chaves de cache que começam com o endpoint base
            var keysToRemove = _cache.Keys.Where(k => 
                k.StartsWith($"GET_{baseEndpoint}") || 
                k.StartsWith($"GET_LIST_{baseEndpoint}")).ToList();
            
            foreach (var key in keysToRemove)
            {
                _logger.LogDebug("Invalidating cache for key {CacheKey}", key);
                _cache.TryRemove(key, out _);
            }
        }

        private bool TryGetFromCache<T>(string key, out T? value)
        {
            if (_cache.TryGetValue(key, out var entry) && !entry.IsExpired)
            {
                value = (T)entry.Value;
                return true;
            }

            value = default;
            return false;
        }

        private void AddToCache<T>(string key, T value, TimeSpan expiration)
        {
            var entry = new CacheEntry(value, DateTime.UtcNow.Add(expiration));
            _cache.AddOrUpdate(key, entry, (_, _) => entry);
        }

        private class CacheEntry
        {
            public object Value { get; }
            public DateTime ExpiresAt { get; }
            public bool IsExpired => DateTime.UtcNow > ExpiresAt;

            public CacheEntry(object value, DateTime expiresAt)
            {
                Value = value;
                ExpiresAt = expiresAt;
            }
        }
    }

    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(string message, Exception innerException, HttpStatusCode statusCode) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, HttpStatusCode statusCode) 
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}