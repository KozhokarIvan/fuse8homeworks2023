namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services
{
    public interface ISettingsService
    {
        /// <summary>
        /// Устанавливает валюту по умолчанию
        /// </summary>
        /// <param name="currencyCode">Код валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task SetDefaultCurrency(string currencyCode, CancellationToken cancellationToken);

        /// <summary>
        /// Устанавливает точность округления
        /// </summary>
        /// <param name="decimalPlaces">Точность округления (количество разрядов после запятой)</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task SetDecimalPlaces(int decimalPlaces, CancellationToken cancellationToken);

        /// <summary>
        /// Получает настройки
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Валюту по умолчанию и точность округления</returns>
        Task<(string defaultCurrency, int decimalPlaces)> GetSettings(CancellationToken cancellationToken);
    }
}
