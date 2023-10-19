using System.Net;
using System.Text.Json;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.ExternalCurrencyApi;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Exceptions;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Options;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsSnapshot<CurrencyApiSettings> _options;
        public CurrencyService(HttpClient httpClient, IOptionsSnapshot<CurrencyApiSettings> options)
        {
            _httpClient = httpClient;
            _options = options;
        }
        public async Task<bool> CheckHealth()
        {
            var response = await _httpClient.GetAsync("status");
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<(string code, decimal value)> GetDefaultCurrency()
        {
            var response = await _httpClient.GetAsync($"latest?currencies={_options.Value.Currency}&base_currency={_options.Value.BaseCurrency}");
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var latestResponse = JsonSerializer.Deserialize<LatestResponse>(responseString, jsonSerializerOptions);
            if (latestResponse is null)
                throw new NullReferenceException();
            var value = decimal.Round(latestResponse.Data[_options.Value.Currency].Value, _options.Value.DecimalPlaces);
            return (latestResponse.Data[_options.Value.Currency].Code, value);
        }
        public async Task<(string code, decimal value)> GetCurrencyByCode(string currencyCode)
        {
            var response = await _httpClient
               .GetAsync($"latest?currencies={currencyCode}&base_currency={_options.Value.BaseCurrency}");
            bool currencyDoesNotExist = response.StatusCode == HttpStatusCode.UnprocessableEntity;
            if (currencyDoesNotExist)
                throw new CurrencyNotFoundException(currencyCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var latestResponse = JsonSerializer.Deserialize<LatestResponse>(responseString, jsonSerializerOptions);
            if (latestResponse is null)
                throw new NullReferenceException();
            var value = decimal.Round(latestResponse.Data[currencyCode].Value, _options.Value.DecimalPlaces);
            return (latestResponse.Data[currencyCode].Code, value);
        }
        public async Task<decimal> GetCurrencyByCodeAndDate(string currencyCode, DateOnly date)
        {
            var response = await _httpClient
                .GetAsync($"historical?currencies={currencyCode}&date={date.ToString("yyyy-MM-dd")}&base_currency={_options.Value.BaseCurrency}");
            bool currencyDoesNotExist = response.StatusCode == HttpStatusCode.UnprocessableEntity;
            if (currencyDoesNotExist)
                throw new CurrencyNotFoundException(currencyCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var currencyResponse = JsonSerializer.Deserialize<HistoricalResponse>(responseString, jsonSerializerOptions);
            if (currencyResponse is null)
                throw new NullReferenceException();
            var value = decimal.Round(currencyResponse.Data[currencyCode].Value, _options.Value.DecimalPlaces);
            return value;
        }
        public async Task<(int requestCount, int requestLimit)> GetRequestQuotas()
        {
            var response = await _httpClient.GetAsync("status");
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var statusResponse = JsonSerializer.Deserialize<StatusResponse>(responseString, jsonSerializerOptions);
            if (statusResponse is null)
                throw new NullReferenceException();
            return (statusResponse.Quotas.Month.Used, statusResponse.Quotas.Month.Total);
        }
    }
}
