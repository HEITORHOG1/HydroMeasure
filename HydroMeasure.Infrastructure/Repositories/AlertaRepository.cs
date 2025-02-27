using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HydroMeasure.Infrastructure.Repositories
{
    public class AlertaRepository : BaseRepository<Alerta>, IAlertaRepository
    {
        public AlertaRepository(HydroMeasureDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Alerta>> GetAlertasByCondominioAsync(Guid condominioId)
        {
            return await _dbSet
                .Include(a => a.Unidade)
                .Include(a => a.UsuarioResolvido)
                .Where(a => a.CondominioId == condominioId)
                .OrderByDescending(a => a.DataAlerta)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alerta>> GetAlertasByUnidadeAsync(Guid unidadeId)
        {
            return await _dbSet
                .Include(a => a.UsuarioResolvido)
                .Where(a => a.UnidadeId == unidadeId)
                .OrderByDescending(a => a.DataAlerta)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alerta>> GetAlertasByTipoAsync(TipoAlerta tipo)
        {
            return await _dbSet
                .Include(a => a.Unidade)
                .Include(a => a.UsuarioResolvido)
                .Where(a => a.Tipo == tipo)
                .OrderByDescending(a => a.DataAlerta)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alerta>> GetAlertasNaoResolvidosAsync(Guid condominioId)
        {
            return await _dbSet
                .Include(a => a.Unidade)
                .Where(a => a.CondominioId == condominioId && !a.Resolvido)
                .OrderByDescending(a => a.DataAlerta)
                .ToListAsync();
        }
    }
}