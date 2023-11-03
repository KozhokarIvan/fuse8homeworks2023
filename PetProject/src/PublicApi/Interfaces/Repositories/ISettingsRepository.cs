namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Repositories
{
    public interface ISettingsRepository
    {
        /// <summary>
        /// Устанавливает валюту по умолчанию
        /// </summary>
        /// <param name="currencyCode">Код валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task SetDefaultCurrency(string currencyCode, CancellationToken cancellationToken);

        /// <summary>
        /// Получает код валюты по умолчанию
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Код валюты по умолчанию</returns>
        Task<string> GetDefaultCurrency(CancellationToken cancellationToken);

        /// <summary>
        /// Установить точность округления (количество разрядов после запятой)
        /// </summary>
        /// <param name="decimalPlacesNumber">Точность округления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task SetDecimalPlaces(int decimalPlacesNumber, CancellationToken cancellationToken);

        /// <summary>
        /// Получает точность округления (количество разрядов после запятой)
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Точность округления</returns>
        Task<int> GetDecimalPlaces(CancellationToken cancellationToken);
    }
}
