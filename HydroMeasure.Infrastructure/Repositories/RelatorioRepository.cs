using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HydroMeasure.Infrastructure.Repositories
{
    public class RelatorioRepository : BaseRepository<Relatorio>, IRelatorioRepository
    {
        public RelatorioRepository(HydroMeasureDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Relatorio>> GetRelatoriosByCondominioAsync(Guid condominioId)
        {
            return await _dbSet
                .Include(r => r.Usuario)
                .Where(r => r.Usuario.CondominioId == condominioId)
                .OrderByDescending(r => r.DataCadastro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Relatorio>> GetRelatoriosByPeriodoAsync(string periodo)
        {
            return await _dbSet
                .Include(r => r.Usuario)
                .Where(r => r.Periodo == periodo)
                .OrderByDescending(r => r.DataCadastro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Relatorio>> GetRelatoriosByTipoAsync(TipoRelatorio tipo)
        {
            return await _dbSet
                .Include(r => r.Usuario)
                .Where(r => r.Tipo == tipo)
                .OrderByDescending(r => r.DataCadastro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Relatorio>> GetRelatoriosByUsuarioAsync(Guid usuarioId)
        {
            return await _dbSet
                .Include(r => r.Usuario)
                .Where(r => r.UsuarioId == usuarioId)
                .OrderByDescending(r => r.DataCadastro)
                .ToListAsync();
        }
    }
}