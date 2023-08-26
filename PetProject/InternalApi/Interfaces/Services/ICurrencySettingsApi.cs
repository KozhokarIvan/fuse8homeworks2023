namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces
{
    public interface ICurrencySettingsApi
    {
        /// <summary>
        /// Получает количество запросов и максимально возможное количество запросов
        /// </summary>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns>Количество запросов и максимально возможное количество запросов</returns>
        Task<(int requestCount, int requestLimit)> GetRequestQuotas(CancellationToken cancellationToken);
    }
}
