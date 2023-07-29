using System.Globalization;
using System.Net;
using System.Text.Json;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Exceptions;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Options;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Services
{
    public class CurrencyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsSnapshot<CurrencyApiSettings> _options;
        public CurrencyService(IHttpClientFactory httpClientFactory, IOptionsSnapshot<CurrencyApiSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }
        public async Task<bool> CheckHealth()
        {
            var httpClient = _httpClientFactory.CreateClient(CurrencyApiSettings.CurrencyApiName);
            var response = await httpClient.GetAsync("status");
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<(string code, decimal value)> GetDefaultCurrency()
        {
            var httpClient = _httpClientFactory.CreateClient(CurrencyApiSettings.CurrencyApiName);
            var response = await httpClient.GetAsync($"latest?currencies={_options.Value.Currency}&base_currency={_options.Value.BaseCurrency}");
            var responseString = await response.Content.ReadAsStringAsync();
            var currencyInfo = GetJsonElementByPath(responseString, "data", _options.Value.Currency);
            string code = currencyInfo.GetProperty("code").ToString();
            decimal value = decimal.Parse(currencyInfo.GetProperty("value").ToString(), CultureInfo.InvariantCulture);
            value = decimal.Round(value, _options.Value.DecimalPlaces);
            return (code, value);
        }
        public async Task<(string code, decimal value)> GetCurrencyByCode(string currencyCode)
        {
            var httpClient = _httpClientFactory.CreateClient(CurrencyApiSettings.CurrencyApiName);
            var response = await httpClient
               .GetAsync($"latest?currencies={currencyCode}&base_currency={_options.Value.BaseCurrency}");
            bool currencyDoesNotExist = response.StatusCode == HttpStatusCode.UnprocessableEntity;
            if (currencyDoesNotExist)
                throw new CurrencyNotFoundException(currencyCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonRootElement = JsonDocument.Parse(responseString).RootElement;
            var currencyInfo = jsonRootElement
                    .GetProperty("data")
                    .GetProperty(currencyCode);
            string code = currencyInfo.GetProperty("code").ToString();
            decimal value = decimal.Parse(currencyInfo.GetProperty("value").ToString(), CultureInfo.InvariantCulture);
            value = decimal.Round(value, _options.Value.DecimalPlaces);
            return (code, value);
        }
        public async Task<decimal> GetCurrencyByCodeAndDate(string currencyCode, DateOnly date)
        {
            var httpClient = _httpClientFactory.CreateClient(CurrencyApiSettings.CurrencyApiName);
            var response = await httpClient
                .GetAsync($"historical?currencies={currencyCode}&date={date.ToString("yyyy-MM-dd")}&base_currency={_options.Value.BaseCurrency}");
            bool currencyDoesNotExist = response.StatusCode == HttpStatusCode.UnprocessableEntity;
            if (currencyDoesNotExist)
                throw new CurrencyNotFoundException(currencyCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonRootElement = JsonDocument.Parse(responseString).RootElement;
            decimal value = decimal
                .Parse(jsonRootElement
                    .GetProperty("data")
                    .GetProperty(currencyCode)
                    .GetProperty("value")
                    .ToString(), CultureInfo.InvariantCulture);
            value = decimal.Round(value, _options.Value.DecimalPlaces);
            return value;
        }
        public async Task<(int requestCount, int requestLimit)> GetRequestQuotas()
        {
            var httpClient = _httpClientFactory.CreateClient(CurrencyApiSettings.CurrencyApiName);
            var response = await httpClient.GetAsync("status");
            var responseString = await response.Content.ReadAsStringAsync();
            var requestsInfo = GetJsonElementByPath(responseString, "quotas", "month");
            var requestsCount = int.Parse(requestsInfo.GetProperty("used").ToString());
            var requestsLimit = int.Parse(requestsInfo.GetProperty("total").ToString());
            return (requestsCount, requestsLimit);
        }
        private JsonElement GetJsonElementByPath(string jsonString, params string[] jsonPath)
        {
            var jsonRootElement = JsonDocument.Parse(jsonString).RootElement;
            JsonElement jsonElement = jsonRootElement.GetProperty(jsonPath[0]);
            for (int i = 1; i < jsonPath.Length; i++)
                jsonElement = jsonElement.GetProperty(jsonPath[i]);
            return jsonElement;
        }
    }
}
