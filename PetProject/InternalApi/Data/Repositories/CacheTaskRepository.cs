using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Repositories
{
    public class CacheTaskRepository : ICacheTaskRepository
    {
        private readonly InternalApiDbContext _context;

        public CacheTaskRepository(InternalApiDbContext context)
        {
            _context = context;
        }

        public async Task CancelTasksByIdsAsync(Guid[] tasksToCancel, CancellationToken cancellationToken)
        {
            await _context.CacheTasks
                .Where(t => tasksToCancel.Contains(t.Id))
                .ExecuteUpdateAsync(s
                    => s.SetProperty(t => t.StatusId, t => (int)CacheTaskStatuses.Cancelled), cancellationToken);
        }

        public async Task<Guid> CreateCacheTask(string newBaseCurrency, CancellationToken cancellationToken)
        {
            var cacheTask = new CacheTask
            {
                CreatedAt = DateTime.UtcNow,
                StatusId = (int)CacheTaskStatuses.Created,
                NewBaseCurrency = newBaseCurrency,
            };
            _context.CacheTasks.Add(cacheTask);
            await _context.SaveChangesAsync(cancellationToken);
            return cacheTask.Id;
        }

        public async Task<CacheTask> GetTaskById(Guid id, CancellationToken cancellationToken)
            => await _context.CacheTasks.AsNoTracking().FirstAsync(t => t.Id == id, cancellationToken);

        public async Task<CacheTask[]> GetUnfinishedCacheTaskIds(CancellationToken cancellationToken)
        {
            var cacheTasks = await _context.CacheTasks
                .AsNoTracking()
                .Where(t
                    => t.StatusId == (int)CacheTaskStatuses.Created || t.StatusId == (int)CacheTaskStatuses.InProgress)
                .ToArrayAsync(cancellationToken);
            return cacheTasks;
        }

        public async Task<CacheTask?> GetPendingTask(CancellationToken cancellationToken)
            => await _context.CacheTasks
            .FirstOrDefaultAsync(t
                => t.StatusId == (int)CacheTaskStatuses.InProgress || t.StatusId == (int)CacheTaskStatuses.Created, cancellationToken);

        public async Task SetTaskStatus(Guid taskId, int taskStatusId, CancellationToken cancellationToken)
        {
            var task = await _context.CacheTasks.FirstAsync(t => t.Id == taskId, cancellationToken);
            task.StatusId = taskStatusId;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
