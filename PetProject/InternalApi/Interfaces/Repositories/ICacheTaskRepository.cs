using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Repositories
{
    public interface ICacheTaskRepository
    {
        /// <summary>
        /// Создает задачу для пересчета кэша относительно новой базовой валюты
        /// </summary>
        /// <param name="newBaseCurrency">Код новой базовой валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<Guid> CreateCacheTask(string newBaseCurrency, CancellationToken cancellationToken);

        /// <summary>
        /// Получает данные задачи для пересчета кэша по ее <paramref name="id"/>
        /// </summary>
        /// <param name="id">Guid задачи</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<CacheTask> GetTaskById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает массив со всеми незавершенными задачами (Статус "создана" или "в обработке")
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<CacheTask[]> GetUnfinishedCacheTaskIds(CancellationToken cancellationToken);

        /// <summary>
        /// Задает задаче с id <paramref name="taskId"/> статус с id <paramref name="taskStatusId"/>
        /// </summary>
        /// <param name="taskId">Guid задачи</param>
        /// <param name="taskStatusId">Id нового статуса задачи</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task SetTaskStatus(Guid taskId, int taskStatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Задает всем задачам guid которых есть в массиве <paramref name="tasksToCancel"/> статус "отменена"
        /// </summary>
        /// <param name="tasksToCancel">Массив с задачами для отмены</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task CancelTasksByIdsAsync(Guid[] tasksToCancel, CancellationToken cancellationToken);

        /// <summary>
        /// Получает самую свежую не завершенную задачу
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<CacheTask?> GetPendingTask(CancellationToken cancellationToken);
    }
}
