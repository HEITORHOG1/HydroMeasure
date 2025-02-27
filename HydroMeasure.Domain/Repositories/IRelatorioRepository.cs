using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories.Base;

namespace HydroMeasure.Domain.Repositories
{
    public interface IRelatorioRepository : IRepository<Relatorio>
    {
        Task<IEnumerable<Relatorio>> GetRelatoriosByCondominioAsync(Guid condominioId);

        Task<IEnumerable<Relatorio>> GetRelatoriosByPeriodoAsync(string periodo);

        Task<IEnumerable<Relatorio>> GetRelatoriosByTipoAsync(TipoRelatorio tipo);

        Task<IEnumerable<Relatorio>> GetRelatoriosByUsuarioAsync(Guid usuarioId);
    }
}