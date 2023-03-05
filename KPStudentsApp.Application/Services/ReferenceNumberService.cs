using KPStudentsApp.Application.Interfaces;
using KPStudentsApp.Domain.Entities;
using KPStudentsApp.Infrastructure.Interfaces;
using KPStudentsApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KPStudentsApp.Application.Services
{
    public class ReferenceNumberService : IReferenceNumberService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IGenericRepository<ReferenceNumber> _refrenceNumberRepository;
        private readonly IDistributedLockManager _distributedLockManager;
        public ReferenceNumberService(
            IGenericRepository<ReferenceNumber> refrenceNumberRepository,
            IDistributedLockManager distributedLockManager,
            ApplicationDbContext context)
        {
            _dbContext = context;
            _distributedLockManager = distributedLockManager;
            _refrenceNumberRepository = refrenceNumberRepository;
        }

        /// <inheritdoc cref="IReferenceNumberService.IncreamentNextValAsync(string, int)"/>
        public async Task<(long, long)> IncreamentNextValAsync(string dimension, int increament = 1)
        {
            if (increament <= 0)
                throw new InvalidOperationException("Reference number next val increament is invalid.");
            var normalizedDimension = dimension?.ToLower() ?? string.Empty;
            (long, long) referenceNumbersRange = (0, 0);

            using (var handle = await _distributedLockManager.AcquireLockAsync(nameof(IReferenceNumberService), TimeSpan.FromHours(1)))
            {
                int records = await _dbContext.Database.ExecuteSqlRawAsync(@"
                                if not exists (select id from referenceNumber where dimension = {0})
                                    begin
                                    insert into referenceNumber(dimension,nextval,createdAt) values({0}, 1 + {1}, GETDATE())
                                    end
                                else
                                    begin
                                        update referenceNumber set nextVal = nextVal + {1} where dimension = {0}
                                    end
                                ", normalizedDimension, increament);

                if (records <= 0)
                    throw new InvalidOperationException("Reference number database update failure.");

                var newNextVal = await _refrenceNumberRepository.Table.AsNoTracking()
                    .Where(x => x.Dimension.ToLower() == normalizedDimension)
                    .Select(x => x.NextVal).FirstOrDefaultAsync();

                referenceNumbersRange = (newNextVal - increament, newNextVal - 1);

                return referenceNumbersRange;
            }
        }
    }
}
