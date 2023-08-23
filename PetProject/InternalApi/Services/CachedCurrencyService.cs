using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services
{
    public class CachedCurrencyService : ICachedCurrencyAPI
    {
        private readonly ICurrencyAPI _currencyService;
        private readonly string _cachePath;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IOptionsSnapshot<CurrencyApiSettings> _options;
        public CachedCurrencyService(ICurrencyAPI currencyService, IOptionsSnapshot<CurrencyApiSettings> options)
        {
            _currencyService = currencyService;
            _options = options;
            _cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _options.Value.RelativeCachePath);
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }
        public async Task<CurrencyDTO> GetCurrencyOnDateAsync(CurrencyCode currencyCode, DateOnly date, CancellationToken cancellationToken)
        {
            CurrencyDTO result;
            if (DoesCacheForCurrencyOnDateExists(date))
            {
                result = GetCurrencyOnDateFromCache(date, currencyCode);
            }
            else
            {
                var currencies = (await _currencyService
                    .GetAllCurrenciesOnDateAsync(_options.Value.BaseCurrency, date, cancellationToken))
                    .Currencies;
                await SaveCurrenciesOnDateToCacheAsync(currencies.Select(c =>
                new CurrencyDTO(Enum.Parse<CurrencyCode>(c.Code), c.Value)).ToArray(), date);
                var currency = currencies
                    .First(c => c.Code.Contains(currencyCode.ToString(), StringComparison.OrdinalIgnoreCase));
                result = new CurrencyDTO(
                    Enum.Parse<CurrencyCode>(currency.Code),
                    currency.Value);
            }
            return result;
        }

        public async Task<CurrencyDTO> GetCurrentCurrencyAsync(CurrencyCode currencyCode, CancellationToken cancellationToken)
        {
            CurrencyDTO result;
            if (IsCacheForCurrentCurrencyRelevant())
            {
                result = GetCurrentCurrencyFromCache(currencyCode);
            }
            else
            {
                var currencies = await _currencyService
                    .GetAllCurrentCurrenciesAsync(_options.Value.BaseCurrency, cancellationToken);
                await SaveCurrenciesToCacheAsync(currencies.Select(c =>
                new CurrencyDTO(Enum.Parse<CurrencyCode>(c.Code), c.Value)).ToArray());
                var currency = currencies
                    .First(c => c.Code.Contains(currencyCode.ToString(), StringComparison.OrdinalIgnoreCase));
                result = new CurrencyDTO(
                    Enum.Parse<CurrencyCode>(currency.Code),
                    currency.Value);
            }
            return result;
        }
        private bool IsCacheForCurrentCurrencyRelevant()
        {
            if (!Directory.Exists(_cachePath))
                return false;
            var currentDateTime = DateTime.Now;
            var correctFiles = Directory.GetFiles(_cachePath)
                .Where(file =>
                DateTime.TryParseExact(
                    Path.GetFileNameWithoutExtension(file),
                    _options.Value.DateTimeFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _))
                .ToArray();
            foreach (var file in correctFiles)
            {
                var dateTime = DateTime
                    .ParseExact(Path.GetFileNameWithoutExtension(file), _options.Value.DateTimeFormat, CultureInfo.InvariantCulture);
                var hoursCacheDifference = currentDateTime.Subtract(dateTime).TotalHours;
                if (hoursCacheDifference < _options.Value.CacheLifeTimeHours)
                    return true;
            }
            return false;
        }
        private CurrencyDTO GetCurrentCurrencyFromCache(CurrencyCode currencyCode)
        {
            var correctFiles = Directory.GetFiles(_cachePath)
               .Where(file =>
               DateTime.TryParseExact(
                   Path.GetFileNameWithoutExtension(file),
                   _options.Value.DateTimeFormat,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out _))
               .ToArray();
            var latestDate = correctFiles
                .Select(file =>
                    DateTime.ParseExact(Path.GetFileNameWithoutExtension(file), _options.Value.DateTimeFormat, CultureInfo.InvariantCulture))
                .OrderByDescending(dt => dt)
                .First();
            var latestCacheFile = correctFiles.First(file => file.Contains(latestDate.ToString(_options.Value.DateTimeFormat)));
            var cachedFileContent = File.ReadAllText(Path.Combine(_cachePath, latestCacheFile));
            var cacheResult = JsonSerializer.Deserialize<CurrencyDTO[]>(cachedFileContent, _jsonSerializerOptions);
            var currency = cacheResult.First(c => c.CurrencyCode == currencyCode);
            return currency;
        }
        private bool DoesCacheForCurrencyOnDateExists(DateOnly date)
        {
            if (!Directory.Exists(_cachePath))
                return false;
            var dateString = date.ToString(_options.Value.DateFormat);
            var files = Directory.GetFiles(_cachePath);
            bool doesCacheExist = files.Any(f => Path.GetFileNameWithoutExtension(f).Contains(dateString));
            return doesCacheExist;
        }
        private CurrencyDTO GetCurrencyOnDateFromCache(DateOnly date, CurrencyCode currencyCode)
        {
            var dateString = date.ToString(_options.Value.DateFormat);
            var correctFiles = Directory.GetFiles(_cachePath)
                .Where(f => Path.GetFileNameWithoutExtension(f).Contains(dateString))
                .ToArray();
            var cacheFile = correctFiles.First(f => f.Contains(dateString));
            var cacheFileContent = File.ReadAllText(Path.Combine(_cachePath, cacheFile));
            var cacheResult = JsonSerializer.Deserialize<CurrencyDTO[]>(cacheFileContent, _jsonSerializerOptions);
            var currency = cacheResult.First(c => c.CurrencyCode == currencyCode);
            return currency;
        }
        private async Task SaveCurrenciesToCacheAsync(CurrencyDTO[] currencies)
        {
            var currenciesString = JsonSerializer.Serialize(currencies, _jsonSerializerOptions);
            await SaveFileAsync(
                Path.Combine(_cachePath, DateTime.Now.ToString(_options.Value.DateTimeFormat)),
                _options.Value.CacheExtension,
                currenciesString);
        }
        private async Task SaveCurrenciesOnDateToCacheAsync(CurrencyDTO[] currencies, DateOnly date)
        {
            var currenciesString = JsonSerializer.Serialize(currencies, _jsonSerializerOptions);
            await SaveFileAsync(
                Path.Combine(_cachePath, date.ToString(_options.Value.DateFormat)),
                _options.Value.CacheExtension,
                currenciesString);
        }
        private async Task SaveFileAsync(string filePath, string extension, string content)
        {
            if (!Directory.Exists(_cachePath))
                Directory.CreateDirectory(_cachePath);
            await File.WriteAllTextAsync(filePath + extension, content);
        }
    }
}
