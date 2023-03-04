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

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task InsertAsync(T entity) => await _dbSet.AddAsync(entity);

        public void InsertRange(List<T> entities) => _dbSet.AddRange(entities);

        public void Update(T entity) => _dbSet.Update(entity);

        public void UpdateRange(List<T> entity) => _dbSet.UpdateRange(entity);

        public async Task<T> GetAsync(int id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
