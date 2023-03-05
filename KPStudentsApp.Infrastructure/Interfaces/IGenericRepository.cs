namespace KPStudentsApp.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> GetAsync(int id);
        Task InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task InsertRange(List<T> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateRange(List<T> entities, CancellationToken cancellationToken = default);
    }
}
