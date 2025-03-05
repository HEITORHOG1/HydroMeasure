namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint, bool useCache = true, TimeSpan? cacheExpiration = null);

        Task<List<T>?> GetListAsync<T>(string endpoint, bool useCache = true, TimeSpan? cacheExpiration = null);

        Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data);

        Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data);

        Task<HttpResponseMessage> DeleteAsync(string endpoint);
    }
}