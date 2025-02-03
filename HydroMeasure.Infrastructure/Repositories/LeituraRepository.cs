using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HydroMeasure.Infrastructure.Repositories
{
    public class LeituraRepository : BaseRepository<Leitura>, ILeituraRepository
    {
        public LeituraRepository(HydroMeasureDbContext context) : base(context)
        {
        }

        public async Task<Leitura?> GetLeituraComDetalhesAsync(Guid id)
        {
            return await _dbSet
                .Include(l => l.Hidrometro)
                .Include(l => l.Unidade)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Leitura>> GetLeiturasPorHidrometroAsync(Guid hidrometroId)
        {
            return await _dbSet
                .Where(l => l.HidrometroId == hidrometroId)
                .OrderByDescending(l => l.DataLeitura)
                .ToListAsync();
        }

        public async Task<IEnumerable<Leitura>> GetLeiturasPorUnidadeAsync(Guid unidadeId)
        {
            return await _dbSet
                .Where(l => l.UnidadeId == unidadeId)
                .OrderByDescending(l => l.DataLeitura)
                .ToListAsync();
        }

        public async Task<Leitura?> GetUltimaLeituraPorHidrometroAsync(Guid hidrometroId)
        {
            return await _dbSet
                .Where(l => l.HidrometroId == hidrometroId)
                .OrderByDescending(l => l.DataLeitura)
                .FirstOrDefaultAsync();
        }
    }
}