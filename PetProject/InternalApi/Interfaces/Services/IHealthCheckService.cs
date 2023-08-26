namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces
{
    public interface IHealthCheckService
    {
        /// <summary>
        /// Возвращает статус работы
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Статус работы</returns>
        Task<bool> CheckHealth(CancellationToken cancellationToken);
    }
}
