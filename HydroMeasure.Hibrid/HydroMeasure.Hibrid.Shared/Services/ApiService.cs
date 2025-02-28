using System.Net.Http.Json;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>(endpoint);
            }
            catch (Exception ex)
            {
                // Podemos adicionar logging aqui
                Console.WriteLine($"Error in GET {endpoint}: {ex.Message}");
                return default;
            }
        }

        public async Task<List<T>?> GetListAsync<T>(string endpoint)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<T>>(endpoint);
            }
            catch (Exception ex)
            {
                // Podemos adicionar logging aqui
                Console.WriteLine($"Error in GET list {endpoint}: {ex.Message}");
                return new List<T>();
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                return await _httpClient.PostAsJsonAsync(endpoint, data);
            }
            catch (Exception ex)
            {
                // Podemos adicionar logging aqui
                Console.WriteLine($"Error in POST {endpoint}: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
        {
            try
            {
                return await _httpClient.PutAsJsonAsync(endpoint, data);
            }
            catch (Exception ex)
            {
                // Podemos adicionar logging aqui
                Console.WriteLine($"Error in PUT {endpoint}: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            try
            {
                return await _httpClient.DeleteAsync(endpoint);
            }
            catch (Exception ex)
            {
                // Podemos adicionar logging aqui
                Console.WriteLine($"Error in DELETE {endpoint}: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
            }
        }
    }
}