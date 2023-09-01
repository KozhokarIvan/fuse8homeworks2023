using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services
{
    public class CacheRecalculatingService : ICacheRecalculatingService
    {
        private readonly ICacheTaskRepository _cacheTaskRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public CacheRecalculatingService(ICacheTaskRepository cacheTaskRepository, ICurrencyRepository currencyRepository)
        {
            _cacheTaskRepository = cacheTaskRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<Guid?> AcceptLastUnfinishedTask(CancellationToken cancellationToken)
        {
            var tasks = await _cacheTaskRepository.GetUnfinishedCacheTaskIds(cancellationToken);
            if (tasks.Length == 0)
                return null;
            if (tasks.Length == 1)
                return tasks.First().Id;
            var latestTask = tasks
                .OrderByDescending(t => t.CreatedAt)
                .First();
            await _cacheTaskRepository
                .CancelTasksByIdsAsync(tasks
                    .Where(t => t.Id != latestTask.Id)
                    .Select(t => t.Id)
                    .ToArray(), cancellationToken);
            return latestTask.Id;
        }

        public async Task RecalculateCacheAsync(Guid taskId, CancellationToken cancellationToken)
        {
            var task = await _cacheTaskRepository.GetTaskById(taskId, cancellationToken);
            await _cacheTaskRepository.SetTaskStatus(taskId, (int)CacheTaskStatuses.InProgress, cancellationToken);
            await _currencyRepository.UpdateCache(task.NewBaseCurrency, cancellationToken);
            await _cacheTaskRepository.SetTaskStatus(taskId, (int)CacheTaskStatuses.Succeeded, cancellationToken);
        }

        public async Task SetTaskFailed(Guid taskId, CancellationToken cancellationToken)
            => await _cacheTaskRepository.SetTaskStatus(taskId, (int)CacheTaskStatuses.Failed, cancellationToken);
    }
}
