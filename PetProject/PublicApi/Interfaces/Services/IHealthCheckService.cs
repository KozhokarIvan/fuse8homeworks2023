namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services
{
    public interface IHealthCheckService
    {
        /// <summary>
        /// Получает статус работы
        /// </summary>
        /// <returns>Статус работы</returns>
        Task<bool> CheckHealth();
    }
}
