using HydroMeasure.Hibrid.Shared.Model.Leitura;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface ILeituraService
    {
        Task<List<LeituraDto>> GetAllLeiturasAsync();

        Task<List<LeituraDto>> GetLeiturasByHidrometroAsync(Guid hidrometroId);

        Task<List<LeituraDto>> GetLeiturasByUnidadeAsync(Guid unidadeId);

        Task<LeituraDto?> GetLeituraByIdAsync(Guid id);

        Task<LeituraDto?> GetUltimaLeituraByHidrometroAsync(Guid hidrometroId);

        Task<OperationResult<LeituraDto>> CreateLeituraAsync(CreateLeituraCommand command);

        Task<OperationResult<LeituraDto>> UpdateLeituraAsync(Guid id, UpdateLeituraCommand command);

        Task<OperationResult<bool>> DeleteLeituraAsync(Guid id);
    }
}