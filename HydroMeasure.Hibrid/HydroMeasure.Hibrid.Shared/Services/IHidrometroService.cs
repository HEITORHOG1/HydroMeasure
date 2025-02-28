using HydroMeasure.Hibrid.Shared.Model.Hidrometro;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface IHidrometroService
    {
        Task<List<HidrometroDto>> GetAllHidrometrosAsync();
        Task<List<HidrometroDto>> GetHidrometrosByUnidadeAsync(Guid unidadeId);
        Task<HidrometroDto?> GetHidrometroByIdAsync(Guid id);
        Task<OperationResult<HidrometroDto>> CreateHidrometroAsync(CreateHidrometroCommand command);
        Task<OperationResult<HidrometroDto>> UpdateHidrometroAsync(Guid id, UpdateHidrometroCommand command);
        Task<OperationResult<bool>> DeleteHidrometroAsync(Guid id);
    }
}
