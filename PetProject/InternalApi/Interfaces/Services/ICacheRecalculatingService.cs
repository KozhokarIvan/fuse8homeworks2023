namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services
{
    public interface ICacheRecalculatingService
    {
        /// <summary>
        /// Пересчитывает все курсы валют в кеше относительно новой базовой валюты
        /// </summary>
        /// <param name="taskId">Guid задачи содержащей информацию о новой базовой валюте</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task RecalculateCacheAsync(Guid taskId, CancellationToken cancellationToken);

        /// <summary>
        /// Устанавливает задаче статус "завершена с ошибкой"
        /// </summary>
        /// <param name="taskId">Guid задачи которую нужно отменить</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task SetTaskFailed(Guid taskId, CancellationToken cancellationToken);

        /// <summary>
        /// Получает guid последней задачи на пересчет кеша (статус создана или в обработке),
        /// а всем остальным с одним из этих статусов меняет его на "отменена"
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<Guid?> AcceptLastUnfinishedTask(CancellationToken cancellationToken);
    }
}
