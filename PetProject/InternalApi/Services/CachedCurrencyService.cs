using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services
{
    public class CachedCurrencyService : ICachedCurrencyAPI
    {
        private readonly ICurrencyAPI _currencyService;
        private readonly IOptionsSnapshot<InternalApiSettings> _options;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICacheTaskRepository _cacheTaskRepository;
        public CachedCurrencyService(
            ICurrencyAPI currencyService,
            IOptionsSnapshot<InternalApiSettings> options,
            ICurrencyRepository currencyRepository,
            ICacheTaskRepository cacheTaskRepository)
        {
            _currencyService = currencyService;
            _options = options;
            _currencyRepository = currencyRepository;
            _cacheTaskRepository = cacheTaskRepository;
        }
        public async Task<Currency> GetCurrencyOnDateAsync(string currencyCode, DateOnly date, CancellationToken cancellationToken)
        {
            var result = await GetCurrencyOnDateFromCacheAsync(date, currencyCode, cancellationToken);
            if (result is not null)
                return result;
            await UpdateCache(cancellationToken);
            var baseCurrency = await _currencyRepository.GetBaseCurrency(cancellationToken);
            var currenciesOnDate = await _currencyService
                    .GetAllCurrenciesOnDateAsync(baseCurrency, date, cancellationToken);
            var allCurrencies = currenciesOnDate.Currencies;
            await SaveCurrenciesOnDateToCacheAsync(allCurrencies.Select(c =>
                new Currency(c.Code, c.Value)).ToArray(), date, cancellationToken);
            var currency = allCurrencies
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
            await UpdateCache(cancellationToken);
            var baseCurrency = await _currencyRepository.GetBaseCurrency(cancellationToken);
            var currencies = await _currencyService
                .GetAllCurrentCurrenciesAsync(baseCurrency, cancellationToken);
            await SaveCurrenciesToCacheAsync(currencies.Select(c =>
                new Currency(c.Code, c.Value)).ToArray(), cancellationToken);
            var currency = currencies
                .First(c => c.Code.Contains(currencyCode, StringComparison.OrdinalIgnoreCase));
            result = new Currency(
                currency.Code,
                currency.Value);
            return result;
        }
        public async Task<decimal> GetFavoriteCurrency(string favoriteBaseCurrencyCode, string favoriteCurrencyCode, CancellationToken cancellationToken)
        {
            var favoriteCurrency = await GetCurrentCurrencyAsync(favoriteCurrencyCode, cancellationToken);
            var favoriteBaseCurrency = await _currencyRepository.GetLatestCurrencyInfo(favoriteBaseCurrencyCode, cancellationToken);
            var baseCurrency = await _currencyRepository.GetBaseCurrency(cancellationToken);
            if (favoriteBaseCurrencyCode.Equals(baseCurrency, StringComparison.InvariantCultureIgnoreCase))
                return favoriteCurrency.Value;
            return favoriteCurrency.Value / favoriteBaseCurrency!.Value;
        }
        public async Task<decimal> GetFavoriteCurrencyOnDate(string favoriteBaseCurrencyCode, string favoriteCurrencyCode, DateOnly date, CancellationToken cancellationToken)
        {
            var favoriteCurrency = await GetCurrencyOnDateAsync(favoriteCurrencyCode, date, cancellationToken);
            var favoriteBaseCurrency = await _currencyRepository.GetLatestCurrencyInfoOnDate(favoriteBaseCurrencyCode, date, cancellationToken);
            var baseCurrency = await _currencyRepository.GetBaseCurrency(cancellationToken);
            if (favoriteBaseCurrencyCode.Equals(baseCurrency, StringComparison.InvariantCultureIgnoreCase))
                return favoriteCurrency.Value;
            return favoriteCurrency.Value / favoriteBaseCurrency!.Value;
        }
        private async Task<Currency?> GetCurrentCurrencyFromCache(string currencyCode, CancellationToken cancellationToken)
        {
            var currencyEntity = await _currencyRepository.GetLatestCurrencyInfo(currencyCode, cancellationToken);
            if (currencyEntity is null)
                return null;
            var currentDateTime = DateTime.UtcNow;
            var dateTime = currencyEntity.DateTime;
            var hoursCacheDifference = currentDateTime.Subtract(dateTime).TotalHours;
            if (hoursCacheDifference < _options.Value.CacheLifeTimeHours)
                return new Currency(currencyCode, currencyEntity.Value);
            return null;
        }
        private async Task<Currency?> GetCurrencyOnDateFromCacheAsync(DateOnly date, string currencyCode, CancellationToken cancellationToken)
        {
            var currencyEntity = await _currencyRepository.GetLatestCurrencyInfoOnDate(currencyCode, date, cancellationToken);
            return currencyEntity is not null
                ? new Currency(currencyCode, currencyEntity.Value)
                : null;
        }
        private async Task UpdateCache(CancellationToken cancellationToken)
        {
            var pendingTask = await _cacheTaskRepository.GetPendingTask(cancellationToken);
            if (pendingTask is null)
                return;
            await Task.Delay(_options.Value.CacheTaskWaitingTimeSeconds * 1_000, cancellationToken);
            pendingTask = await _cacheTaskRepository.GetPendingTask(cancellationToken);
            if (pendingTask is not null)
                throw new Exception("Данные в кеше не были полностью обновлены");
        }
        private async Task SaveCurrenciesToCacheAsync(Currency[] currencies, CancellationToken cancellationToken)
        {
            var currenciesDictionary = currencies.ToDictionary(
               el => el.Code,
               el => el.Value);
            await _currencyRepository.AddCurrentCurrencies(currenciesDictionary, cancellationToken);
        }
        private async Task SaveCurrenciesOnDateToCacheAsync(Currency[] currencies, DateOnly date, CancellationToken cancellationToken)
        {
            var currenciesDictionary = currencies.ToDictionary(
                el => el.Code,
                el => el.Value);
            await _currencyRepository.AddCurrenciesOnDate(currenciesDictionary, date, cancellationToken);
        }
    }
}
