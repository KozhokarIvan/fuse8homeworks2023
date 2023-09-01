using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services.Background;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services
{
    public class CacheTasksService : ICacheTasksService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ICacheTaskRepository _taskRepository;

        public CacheTasksService(IBackgroundTaskQueue taskQueue, ICacheTaskRepository taskRepository)
        {
            _taskQueue = taskQueue;
            _taskRepository = taskRepository;
        }

        public async Task<Guid> AddTaskAsync(string newBaseCurrency, CancellationToken cancellationToken)
        {
            var taskId = await _taskRepository.CreateCacheTask(newBaseCurrency, cancellationToken);
            await _taskQueue.QueueAsync(new WorkItem(taskId), cancellationToken);
            return taskId;
        }
    }
}
