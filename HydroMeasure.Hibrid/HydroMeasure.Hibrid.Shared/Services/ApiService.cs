using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ApiService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiService(HttpClient httpClient, IMemoryCache cache, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
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
            if (useCache && _cache.TryGetValue(cacheKey, out T? cachedResult))
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
                var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);

                // Armazenar em cache se habilitado
                if (useCache && result != null)
                {
                    var expiration = cacheExpiration ?? TimeSpan.FromMinutes(5);
                    _cache.Set(cacheKey, result, expiration);
                    _logger.LogDebug("Cached result for {Endpoint} with expiration {Expiration}", endpoint, expiration);
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network error in GET {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException($"Erro de conexão: {ex.Message}", ex, HttpStatusCode.ServiceUnavailable);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error in GET {Endpoint}: {Message}", endpoint, ex.Message);
                throw new ApiException("Erro ao processar resposta da API", ex, HttpStatusCode.UnprocessableEntity);
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
            if (useCache && _cache.TryGetValue(cacheKey, out List<T>? cachedResult))
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
                    _cache.Set(cacheKey, result, expiration);
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

            // Buscar todas as chaves de cache que começam com o endpoint base
            var cacheKeys = _cache.GetKeys<string>().Where(k =>
                k.StartsWith($"GET_{baseEndpoint}") ||
                k.StartsWith($"GET_LIST_{baseEndpoint}"));

            foreach (var key in cacheKeys)
            {
                _logger.LogDebug("Invalidating cache for key {CacheKey}", key);
                _cache.Remove(key);
            }
        }
    }

    // Extensão para obter todas as chaves do cache
    public static class MemoryCacheExtensions
    {
        public static IEnumerable<T> GetKeys<T>(this IMemoryCache cache)
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var collection = field?.GetValue(cache) as dynamic;

            if (collection != null)
            {
                var items = new List<T>();
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var key = methodInfo.GetValue(item);
                    if (key is T keyT)
                    {
                        items.Add(keyT);
                    }
                }
                return items;
            }

            return Enumerable.Empty<T>();
        }
    }

    // Classe de exceção personalizada para erros de API
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