using System.Net;
using System.Text.Json;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.ExternalCurrencyApi;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Exceptions;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services
{
    public class CurrencyService : ICurrencyAPI, ICurrencySettingsApi, IHealthCheckService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsSnapshot<InternalApiSettings> _options;
        public CurrencyService(HttpClient httpClient, IOptionsSnapshot<InternalApiSettings> options)
        {
            _httpClient = httpClient;
            _options = options;
        }
        public async Task<bool> CheckHealth(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync("status", cancellationToken);
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<(int requestCount, int requestLimit)> GetRequestQuotas(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync("status", cancellationToken);
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var statusResponse =
                JsonSerializer.Deserialize<StatusResponse>(responseString, jsonSerializerOptions)
                ?? throw new NullReferenceException();
            return (statusResponse.Quotas.Month.Used, statusResponse.Quotas.Month.Total);
        }

        public async Task<Models.Currency[]> GetAllCurrentCurrenciesAsync(string baseCurrency, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                $"latest?base_currency={_options.Value.BaseCurrency}",
                cancellationToken);
            bool currencyDoesNotExist = response.StatusCode == HttpStatusCode.UnprocessableEntity;
            if (currencyDoesNotExist)
                throw new CurrencyNotFoundException(baseCurrency);
            bool tooManyRequests = response.StatusCode == HttpStatusCode.TooManyRequests;
            if (tooManyRequests)
                throw new ApiRequestLimitException();
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var statusResponse =
                JsonSerializer.Deserialize<CurrenciesResponse>(responseString, jsonSerializerOptions)
                ?? throw new NullReferenceException();
            var result = statusResponse.Data.Values
                .Select(currency => new Models.Currency(currency.Code, currency.Value))
                .ToArray();
            return result;
        }

        public async Task<CurrenciesOnDate> GetAllCurrenciesOnDateAsync(string baseCurrency, DateOnly date, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                $"historical?base_currency={_options.Value.BaseCurrency}&date={date:yyyy-MM-dd}",
                cancellationToken);
            bool currencyDoesNotExist = response.StatusCode == HttpStatusCode.UnprocessableEntity;
            if (currencyDoesNotExist)
                throw new CurrencyNotFoundException(baseCurrency);
            bool tooManyRequests = response.StatusCode == HttpStatusCode.TooManyRequests;
            if (tooManyRequests)
                throw new ApiRequestLimitException();
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var statusResponse =
                JsonSerializer.Deserialize<HistoricalResponse>(responseString, jsonSerializerOptions)
                ?? throw new NullReferenceException();
            var currencies = statusResponse.Data.Values
                .Select(currency => new Models.Currency(currency.Code, currency.Value))
                .ToArray();
            var result = new CurrenciesOnDate(statusResponse.Meta.LastUpdatedAt, currencies);
            return result;
        }
    }
}
