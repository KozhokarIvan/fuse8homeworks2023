using Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces;

public interface ICachedCurrencyAPI
{
    /// <summary>
    /// Получает текущий курс
    /// </summary>
    /// <param name="currencyType">Валюта, для которой необходимо получить курс</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Текущий курс</returns>
    public Task<CurrencyDTO> GetCurrentCurrencyAsync(CurrencyCode currencyType, CancellationToken cancellationToken);

    /// <summary>
    /// Получает курс валюты, актуальный на <paramref name="date"/>
    /// </summary>
    /// <param name="currencyCode">Валюта, для которой необходимо получить курс</param>
    /// <param name="date">Дата, на которую нужно получить курс валют</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Курс на дату</returns>
    public Task<CurrencyDTO> GetCurrencyOnDateAsync(CurrencyCode currencyCode, DateOnly date, CancellationToken cancellationToken);
}