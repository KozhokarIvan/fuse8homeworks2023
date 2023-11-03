using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services.Background;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Background
{
    public class CacheTasksBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<CacheTasksBackgroundService> _logger;
        private readonly IBackgroundTaskQueue _taskQueue;

        public CacheTasksBackgroundService(
            IServiceProvider services,
            ILogger<CacheTasksBackgroundService> logger,
            IBackgroundTaskQueue taskQueue)
        {
            _services = services;
            _logger = logger;
            _taskQueue = taskQueue;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);
                using var scope = _services.CreateScope();
                try
                {
                    var cacheRecalculatingService = scope.ServiceProvider.GetRequiredService<ICacheRecalculatingService>();
                    await cacheRecalculatingService.RecalculateCacheAsync(workItem.TaskId, stoppingToken);
                    _logger.LogInformation("Кэш был успешно пересчитан, Guid задачи: {guid}", workItem.TaskId);
                }
                catch (Exception ex)
                {
                    var cacheRecalculatingService = scope.ServiceProvider.GetRequiredService<ICacheRecalculatingService>();
                    await cacheRecalculatingService.SetTaskFailed(workItem.TaskId, stoppingToken);
                    _logger.LogError(ex, "Произошла ошибка во время работы с пересчетом кэша, Guid задачи: {@taskGuid}", workItem.TaskId);
                }
            }
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var cacheRecalculatingService = scope.ServiceProvider.GetRequiredService<ICacheRecalculatingService>();
            var taskId = await cacheRecalculatingService.AcceptLastUnfinishedTask(cancellationToken);
            if (!taskId.HasValue)
                return;
            await _taskQueue.QueueAsync(new WorkItem(taskId.Value), cancellationToken);
            await base.StartAsync(cancellationToken);
        }
    }
}
