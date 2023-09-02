using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        /// <summary>
        ///  Получает последний курс для выбранной валюты
        /// </summary>
        /// <param name="currencyCode">Код валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Последний курс валюты с кодом <paramref name="currencyCode"/></returns>
        Task<Currency?> GetLatestCurrencyInfo(string currencyCode, CancellationToken cancellationToken);

        /// <summary>
        ///  Получает курс для выбранной валюты на выбранную дату
        /// </summary>
        /// <param name="currencyCode">Код валюты</param>
        /// <param name="date">Дата</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Курс валюты с кодом <paramref name="currencyCode"/> актуальный на дату <paramref name="date"/></returns>
        Task<Currency?> GetCurrencyInfoOnDate(string currencyCode, DateOnly date, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет курсы всех переданных валют относительно базового
        /// </summary>
        /// <param name="currencies">Список валют и их курсов относительно базовой валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task AddCurrentCurrencies(Dictionary<string, decimal> currencies, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет курсы всех переданных валют относительно базового на выбранную дату
        /// </summary>
        /// <param name="currencies">Список валют и их курсов относительно базовой валюты</param>
        /// <param name="date">Дата</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task AddCurrenciesOnDate(Dictionary<string, decimal> currencies, DateOnly date, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает код базовой валюты
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<string> GetBaseCurrency(CancellationToken cancellationToken);

        /// <summary>
        /// Пересчитывает все курсы валют относительно <paramref name="newBaseCurrency"/>, изменяя при этом базовый на <paramref name="newBaseCurrency"/>
        /// в рамках одной транзакции
        /// </summary>
        /// <param name="newBaseCurrency">Код новой базовой валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task UpdateCache(string newBaseCurrency, CancellationToken cancellationToken);
    }
}
