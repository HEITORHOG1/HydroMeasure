namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint);

        Task<List<T>?> GetListAsync<T>(string endpoint);

        Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data);

        Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data);

        Task<HttpResponseMessage> DeleteAsync(string endpoint);
    }
}