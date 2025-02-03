using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories.Base;

namespace HydroMeasure.Domain.Repositories
{
    public interface IHidrometroRepository : IRepository<Hidrometro>
    {
        // Métodos específicos de Hidrometro (se necessário)
        Task<Hidrometro?> GetHidrometroWithDetailsAsync(Guid id);
    }
}