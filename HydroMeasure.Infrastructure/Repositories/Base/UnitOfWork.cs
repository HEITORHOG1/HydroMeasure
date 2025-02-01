using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Infrastructure.Context;

namespace HydroMeasure.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HydroMeasureDbContext _context;

        public UnitOfWork(HydroMeasureDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}