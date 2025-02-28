using HydroMeasure.Hibrid.Shared.Model.Unidade;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface IUnidadeService
    {
        Task<List<UnidadeDto>> GetAllUnidadesAsync();

        Task<List<UnidadeDto>> GetUnidadesByCondominioAsync(Guid condominioId);

        Task<UnidadeDto?> GetUnidadeByIdAsync(Guid id);

        Task<OperationResult<UnidadeDto>> CreateUnidadeAsync(CreateUnidadeCommand command);

        Task<OperationResult<UnidadeDto>> UpdateUnidadeAsync(Guid id, UpdateUnidadeCommand command);

        Task<OperationResult<bool>> DeleteUnidadeAsync(Guid id);
    }
}