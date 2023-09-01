using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Repositories
{
    public interface ICacheTaskRepository
    {
        Task<Guid> CreateCacheTask(string newBaseCurrency, CancellationToken cancellationToken);
        Task<CacheTask> GetTaskById(Guid id, CancellationToken cancellationToken);
        Task<CacheTask[]> GetUnfinishedCacheTaskIds(CancellationToken cancellationToken);
        Task SetTaskStatus(Guid taskId, int taskStatusId, CancellationToken cancellationToken);
        Task CancelTasksByIdsAsync(Guid[] tasksToCancel, CancellationToken cancellationToken);
        Task<CacheTask?> GetPendingTask(CancellationToken cancellationToken);
    }
}
