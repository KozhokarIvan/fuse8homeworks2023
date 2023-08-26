namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Получает курс валюты по умолчанию относительно базового
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Код валюты и курс относительно базового</returns>
        Task<(string code, decimal value)> GetDefaultCurrency(CancellationToken cancellationToken);

        /// <summary>
        /// Получает курс валюты относительно базового
        /// </summary>
        /// <param name="currencyCode">Код валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Код валюты и ее курс относительно базового</returns>
        Task<(string code, decimal value)> GetCurrencyByCode(string currencyCode, CancellationToken cancellationToken);

        /// <summary>
        /// Получает курс валюты относительно базового
        /// </summary>
        /// <param name="currencyCode">Код валюты</param>
        /// <param name="date">дата</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Курс валюты <paramref name="currencyCode"/> относительно базового</returns>
        Task<decimal> GetCurrencyOnDate(string currencyCode, DateOnly date, CancellationToken cancellationToken);

        /// <summary>
        /// Получает курс для избранной пары валют
        /// </summary>
        /// <param name="favoriteExchangeName">Название избранной пары валют</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Курс для избранной пары валют</returns>
        Task<decimal?> GetFavoriteCurrency(string favoriteExchangeName, CancellationToken cancellationToken);

        /// <summary>
        /// Получает курс для избранной пары валют относительно базового на дату
        /// </summary>
        /// <param name="favoriteExchangeName">Название для избранной пары валют</param>
        /// <param name="date">Дата</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Курс для избранной пары валют актуальный на дату <paramref name="date"/></returns>
        Task<decimal?> GetFavoriteCurrencyOnDate(string favoriteExchangeName, DateOnly date, CancellationToken cancellationToken);

        /// <summary>
        /// Получает параметры для запросов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Базовая валюта и остались ли запросы к внешнему API в тарифе, если нет данных в кеше InternalAPI</returns>
        Task<(string baseCurrency, bool canRequest)> GetRequestQuotas(CancellationToken cancellationToken);
    }
}
