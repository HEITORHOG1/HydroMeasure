using HydroMeasure.Hibrid.Shared.Model.Condominio;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface ICondominioService
    {
        Task<List<CondominioDto>> GetAllCondominiosAsync();

        Task<CondominioDto?> GetCondominioByIdAsync(Guid id);

        Task<OperationResult<CondominioDto>> CreateCondominioAsync(CreateCondominioCommand command);

        Task<OperationResult<CondominioDto>> UpdateCondominioAsync(Guid id, UpdateCondominioCommand command);

        Task<OperationResult<bool>> DeleteCondominioAsync(Guid id);
    }
}