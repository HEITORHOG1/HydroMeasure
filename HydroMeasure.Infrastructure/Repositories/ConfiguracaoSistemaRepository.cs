using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HydroMeasure.Infrastructure.Repositories
{
    public class ConfiguracaoSistemaRepository : BaseRepository<ConfiguracaoSistema>, IConfiguracaoSistemaRepository
    {
        private readonly HydroMeasureDbContext _hydroMeasureContext;

        public ConfiguracaoSistemaRepository(HydroMeasureDbContext context) : base(context)
        {
            _hydroMeasureContext = context;
        }

        public async Task<ConfiguracaoSistema?> GetAsync()
        {
            // Recupera a primeira configuração do sistema disponível
            // Normalmente deveria existir apenas uma configuração no sistema
            return await _hydroMeasureContext.ConfiguracoesSistema.FirstOrDefaultAsync();
        }

        public async Task<ConfiguracaoSistema?> GetByIdAsync(Guid id)
        {
            return await _hydroMeasureContext.ConfiguracoesSistema.FindAsync(id);
        }

        public async Task<ConfiguracaoSistema> AddAsync(ConfiguracaoSistema configuracao)
        {
            await _hydroMeasureContext.ConfiguracoesSistema.AddAsync(configuracao);
            await _hydroMeasureContext.SaveChangesAsync();
            return configuracao;
        }

        public async Task<ConfiguracaoSistema> UpdateAsync(ConfiguracaoSistema configuracao)
        {
            _hydroMeasureContext.Entry(configuracao).State = EntityState.Modified;
            await _hydroMeasureContext.SaveChangesAsync();
            return configuracao;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var configuracao = await _hydroMeasureContext.ConfiguracoesSistema.FindAsync(id);
            if (configuracao == null)
                return false;

            _hydroMeasureContext.ConfiguracoesSistema.Remove(configuracao);
            await _hydroMeasureContext.SaveChangesAsync();
            return true;
        }
    }
}