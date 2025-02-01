using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories.Base;

namespace HydroMeasure.Domain.Repositories
{
    public interface ICondominioRepository : IRepository<Condominio>
    {
        Task<Condominio?> GetCondominioWithDetailsAsync(Guid id);
    }
}