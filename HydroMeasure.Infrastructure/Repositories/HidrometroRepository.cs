using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HydroMeasure.Infrastructure.Repositories
{
    public class HidrometroRepository : BaseRepository<Hidrometro>, IHidrometroRepository
    {
        public HidrometroRepository(HydroMeasureDbContext context) : base(context)
        {
        }

        public async Task<Hidrometro?> GetHidrometroWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(h => h.Leituras) // Exemplo: Incluir Leituras se necessário no futuro
                .FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}