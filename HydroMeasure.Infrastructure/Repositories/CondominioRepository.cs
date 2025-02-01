using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HydroMeasure.Infrastructure.Repositories
{
    public class CondominioRepository : BaseRepository<Condominio>, ICondominioRepository
    {
        public CondominioRepository(HydroMeasureDbContext context) : base(context)
        {
        }

        public async Task<Condominio?> GetCondominioWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Unidades)
                .Include(c => c.Usuarios)
                .Include(c => c.Alertas)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}