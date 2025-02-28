using HydroMeasure.Hibrid.Shared.Model.Alerta;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface IAlertaService
    {
        Task<List<AlertaDto>> GetAlertasByCondominioAsync(Guid condominioId);

        Task<List<AlertaDto>> GetAlertasByUnidadeAsync(Guid unidadeId);

        Task<List<AlertaDto>> GetAlertasNaoResolvidosAsync(Guid condominioId);

        Task<AlertaDto?> GetAlertaByIdAsync(Guid id);
    }
}