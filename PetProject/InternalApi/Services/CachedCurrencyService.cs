using System.Globalization;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services
{
    public class CachedCurrencyService : ICachedCurrencyAPI
    {
        private readonly ICurrencyAPI _currencyService;
        private readonly IOptionsSnapshot<InternalApiSettings> _options;
        private readonly ICurrencyRepository _currencyRepository;
        public CachedCurrencyService(ICurrencyAPI currencyService, IOptionsSnapshot<InternalApiSettings> options, ICurrencyRepository currencyRepository)
        {
            _currencyService = currencyService;
            _options = options;
            _currencyRepository = currencyRepository;
        }
        public async Task<Currency> GetCurrencyOnDateAsync(string currencyCode, DateOnly date, CancellationToken cancellationToken)
        {
            var result = await GetCurrencyOnDateFromCacheAsync(date, currencyCode, cancellationToken);
            if (result is not null)
                return result;
            var currencies = (await _currencyService
                    .GetAllCurrenciesOnDateAsync(_options.Value.BaseCurrency, date, cancellationToken))
                    .Currencies;
            await SaveCurrenciesOnDateToCacheAsync(currencies.Select(c =>
                new Currency(c.Code, c.Value)).ToArray(), date, cancellationToken);
            var currency = currencies
                .First(c => c.Code.Contains(currencyCode, StringComparison.OrdinalIgnoreCase));
            result = new Currency(
                currency.Code,
                currency.Value);
            return result;
        }
        public async Task<Currency> GetCurrentCurrencyAsync(string currencyCode, CancellationToken cancellationToken)
        {
            var result = await GetCurrentCurrencyFromCache(currencyCode, cancellationToken);
            if (result is not null)
                return result;
            var currencies = await _currencyService
                .GetAllCurrentCurrenciesAsync(_options.Value.BaseCurrency, cancellationToken);
            await SaveCurrenciesToCacheAsync(currencies.Select(c =>
            new Currency(c.Code, c.Value)).ToArray(), cancellationToken);
            var currency = currencies
                .First(c => c.Code.Contains(currencyCode, StringComparison.OrdinalIgnoreCase));
            result = new Currency(
                currency.Code,
                currency.Value);
            return result;
        }
        private async Task<Currency?> GetCurrentCurrencyFromCache(string currencyCode, CancellationToken cancellationToken)
        {
            var currencyEntity = await _currencyRepository.GetLatestCurrencyInfo(currencyCode, cancellationToken);
            if (currencyEntity is null)
                return null;
            var currentDateTime = DateTime.UtcNow;
            var dateTime = currencyEntity.DateTime;
            var hoursCacheDifference = currentDateTime.Subtract(dateTime).TotalHours;
            if (hoursCacheDifference > 2) 
                return null;
            return new Currency(currencyCode, currencyEntity.Value);
        }
        private async Task<Currency?> GetCurrencyOnDateFromCacheAsync(DateOnly date, string currencyCode, CancellationToken cancellationToken)
        {
            var currencyEntity = await _currencyRepository.GetCurrencyInfoOnDate(currencyCode, date, cancellationToken);
            return currencyEntity is not null
                ? new Currency(currencyCode, currencyEntity.Value)
                : null;
        }
        private async Task SaveCurrenciesToCacheAsync(Currency[] currencies, CancellationToken cancellationToken)
        {
            var currenciesDictionary = currencies.ToDictionary(
               el => el.Code,
               el => el.Value);
            await _currencyRepository.AddCurrentCurrencies(currenciesDictionary, _options.Value.BaseCurrency, cancellationToken);
        }
        private async Task SaveCurrenciesOnDateToCacheAsync(Currency[] currencies, DateOnly date, CancellationToken cancellationToken)
        {
            var currenciesDictionary = currencies.ToDictionary(
                el => el.Code,
                el => el.Value);
            await _currencyRepository.AddCurrenciesOnDate(currenciesDictionary, _options.Value.BaseCurrency, date, cancellationToken);
        }

        public async Task<decimal> GetFavoriteCurrency(string favoriteBaseCurrencyCode, string favoriteCurrencyCode, CancellationToken cancellationToken)
        {
            var favoriteCurrency = await GetCurrentCurrencyAsync(favoriteCurrencyCode, cancellationToken);
            var favoriteBaseCurrency = await _currencyRepository.GetLatestCurrencyInfo(favoriteBaseCurrencyCode, cancellationToken);
            if (favoriteBaseCurrency!.BaseCurrencyCode.Name.Equals(favoriteBaseCurrencyCode, StringComparison.InvariantCultureIgnoreCase))
                return favoriteCurrency.Value;
            return favoriteCurrency.Value / favoriteBaseCurrency.Value;
        }

        public async Task<decimal> GetFavoriteCurrencyOnDate(string favoriteBaseCurrencyCode, string favoriteCurrencyCode, DateOnly date, CancellationToken cancellationToken)
        {
            var favoriteCurrency = await GetCurrencyOnDateAsync(favoriteCurrencyCode, date, cancellationToken);
            var favoriteBaseCurrency = await _currencyRepository.GetCurrencyInfoOnDate(favoriteBaseCurrencyCode, date, cancellationToken);
            if (favoriteBaseCurrency!.BaseCurrencyCode.Name.Equals(favoriteBaseCurrencyCode, StringComparison.InvariantCultureIgnoreCase))
                return favoriteCurrency.Value;
            return favoriteCurrency.Value / favoriteBaseCurrency.Value;
        }
    }
}
