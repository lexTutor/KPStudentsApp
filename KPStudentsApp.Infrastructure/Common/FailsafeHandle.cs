using Medallion.Threading;
using Microsoft.Extensions.Logging;

namespace KPStudentsApp.Infrastructure.Common
{
    public class FailsafeHandle : IDistributedSynchronizationHandle
    {
        private readonly IDistributedSynchronizationHandle _handle;
        private readonly ILogger _logger;

        public FailsafeHandle(IDistributedSynchronizationHandle handle,
            ILogger logger)
        {
            _handle = handle;
            _logger = logger;
        }

        public CancellationToken HandleLostToken => _handle.HandleLostToken;

        public void Dispose()
        {
            try
            {
                _handle.Dispose();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);

                // Fail silently; if e.g the DB Connection failed, lock is lost anyway
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                await _handle.DisposeAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);

                // Fail silently; if e.g the DB Connection failed, lock is lost anyway
            }
        }
    }
}
