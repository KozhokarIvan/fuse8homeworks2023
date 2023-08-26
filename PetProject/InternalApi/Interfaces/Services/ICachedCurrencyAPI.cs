using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces;

public interface ICachedCurrencyAPI
{
    /// <summary>
    /// Получает текущий курс
    /// </summary>
    /// <param name="currencyType">Валюта, для которой необходимо получить курс</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Текущий курс</returns>
    Task<Currency> GetCurrentCurrencyAsync(string currencyType, CancellationToken cancellationToken);

    /// <summary>
    /// Получает курс валюты, актуальный на <paramref name="date"/>
    /// </summary>
    /// <param name="currencyCode">Валюта, для которой необходимо получить курс</param>
    /// <param name="date">Дата, на которую нужно получить курс валют</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Курс на дату <paramref name="date"/></returns>
    Task<Currency> GetCurrencyOnDateAsync(string currencyCode, DateOnly date, CancellationToken cancellationToken);

    /// <summary>
    /// Получает текущий курс для избранной пары валют
    /// </summary>
    /// <param name="favoriteBaseCurrencyCode">Базовая валюта для избранной пары</param>
    /// <param name="favoriteCurrencyCode">Валюта избранной пары</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Текущий курс для избранной пары валют</returns>
    Task<decimal> GetFavoriteCurrency(string favoriteBaseCurrencyCode, string favoriteCurrencyCode, CancellationToken cancellationToken);

    /// <summary>
    /// Получает курс для избранной пары валют на выбранную дату <paramref name="date"/>
    /// </summary>
    /// <param name="favoriteBaseCurrencyCode">Базовая валюта для избранной пары</param>
    /// <param name="favoriteCurrencyCode">Валюта избранной пары</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Курс для избранной пары валют на дату <paramref name="date"/></returns>
    Task<decimal> GetFavoriteCurrencyOnDate(string favoriteBaseCurrencyCode, string favoriteCurrencyCode, DateOnly date, CancellationToken cancellationToken);
}