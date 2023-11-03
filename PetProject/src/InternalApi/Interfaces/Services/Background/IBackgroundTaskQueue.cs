using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services.Background
{
    public interface IBackgroundTaskQueue
    {
        /// <summary>
        /// Добавляет задачу в очередь на выполнение
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        ValueTask QueueAsync(WorkItem command, CancellationToken cancellationToken);

        /// <summary>
        /// Извлекает задачу из очереди на выполнение
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        ValueTask<WorkItem> DequeueAsync(CancellationToken cancellationToken);
    }
}
