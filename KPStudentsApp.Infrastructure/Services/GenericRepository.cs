using KPStudentsApp.Domain.Common;
using KPStudentsApp.Infrastructure.Interfaces;
using KPStudentsApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KPStudentsApp.Infrastructure.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public IQueryable<T> Table => _dbSet;

        public IQueryable<T> TableNoTracking => _dbSet.AsNoTracking();

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task InsertRange(List<T> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.AddRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRange(List<T> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> GetAsync(int id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }
}
