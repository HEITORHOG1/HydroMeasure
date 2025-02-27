using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories.Base;

namespace HydroMeasure.Domain.Repositories
{
    public interface IAlertaRepository : IRepository<Alerta>
    {
        Task<IEnumerable<Alerta>> GetAlertasByCondominioAsync(Guid condominioId);

        Task<IEnumerable<Alerta>> GetAlertasByUnidadeAsync(Guid unidadeId);

        Task<IEnumerable<Alerta>> GetAlertasByTipoAsync(TipoAlerta tipo);

        Task<IEnumerable<Alerta>> GetAlertasNaoResolvidosAsync(Guid condominioId);
    }
}