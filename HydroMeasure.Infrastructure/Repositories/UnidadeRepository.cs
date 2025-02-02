using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HydroMeasure.Infrastructure.Repositories
{
    public class UnidadeRepository : BaseRepository<Unidade>, IUnidadeRepository
    {
        public UnidadeRepository(HydroMeasureDbContext context) : base(context)
        {
        }

        public async Task<Unidade?> GetUnidadeWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(u => u.Hidrometros)
                .Include(u => u.Leituras)
                .Include(u => u.Alertas)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}