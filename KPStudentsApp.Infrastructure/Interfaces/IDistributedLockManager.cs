using Medallion.Threading;

namespace KPStudentsApp.Infrastructure.Interfaces
{
    public interface IDistributedLockManager
    {
        Task<IDistributedSynchronizationHandle?> AcquireLockAsync(string name, TimeSpan? timeout = null, CancellationToken cancellationToken = default);
    }
}
