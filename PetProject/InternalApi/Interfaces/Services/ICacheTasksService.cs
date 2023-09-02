namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services
{
    public interface ICacheTasksService
    {
        /// <summary>
        /// Создает задачу на пересчет кэша
        /// </summary>
        /// <param name="newBaseCurrency">Id новой базовой валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<Guid> AddTaskAsync(string newBaseCurrency, CancellationToken cancellationToken);
    }
}
