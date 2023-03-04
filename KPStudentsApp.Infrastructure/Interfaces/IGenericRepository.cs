namespace KPStudentsApp.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetAsync(int id);
        void InsertRange(List<T> entities);
        void UpdateRange(List<T> entity);
        Task SaveAsync(CancellationToken cancellationToken = default);

        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
}
