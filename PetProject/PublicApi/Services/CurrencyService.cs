using System.Net;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Exceptions;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Grpc.Contracts;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Options;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly GrpcCurrency.GrpcCurrencyClient _grpcClient;
        private readonly IOptionsSnapshot<CurrencyApiSettings> _options;
        public CurrencyService(HttpClient httpClient, GrpcCurrency.GrpcCurrencyClient grpcClient, IOptionsSnapshot<CurrencyApiSettings> options)
        {
            _httpClient = httpClient;
            _grpcClient = grpcClient;
            _options = options;
        }
        public async Task<bool> CheckHealth()
        {
            var response = await _httpClient.GetAsync("status");
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<(string code, decimal value)> GetDefaultCurrency()
        {
            var grpcResponse = await _grpcClient.GetCurrentCurrencyAsync(new Grpc.Contracts.GetCurrentCurrencyRequest()
            {
                CurrencyCode = _options.Value.Currency
            });
            switch (grpcResponse.StatusCode)
            {
                case Grpc.Contracts.StatusCodes.NoError:
                    var value = decimal.Round(grpcResponse.Value, _options.Value.DecimalPlaces);
                    return (grpcResponse.CurrencyCode, value);
                case Grpc.Contracts.StatusCodes.UnknownCurrency:
                    throw new CurrencyNotFoundException(_options.Value.Currency);
                case Grpc.Contracts.StatusCodes.NoRequestsLeft:
                    throw new ApiRequestLimitException();
                default:
                    throw new Exception("Unknown status code returned from grpc service");
            }
        }
        public async Task<(string code, decimal value)> GetCurrencyByCode(string currencyCode)
        {
            var grpcResponse = await _grpcClient.GetCurrentCurrencyAsync(new GetCurrentCurrencyRequest()
            {
                CurrencyCode = currencyCode
            });
            switch (grpcResponse.StatusCode)
            {
                case Grpc.Contracts.StatusCodes.NoError:
                    var value = decimal.Round(grpcResponse.Value, _options.Value.DecimalPlaces);
                    return (grpcResponse.CurrencyCode, value);
                case Grpc.Contracts.StatusCodes.UnknownCurrency:
                    throw new CurrencyNotFoundException(_options.Value.Currency);
                case Grpc.Contracts.StatusCodes.NoRequestsLeft:
                    throw new ApiRequestLimitException();
                default:
                    throw new Exception("Unknown status code returned from grpc service");
            }
        }
        public async Task<decimal> GetCurrencyOnDate(string currencyCode, DateOnly date)
        {
            Timestamp protoTimestamp = Timestamp.FromDateTime(
                DateTime.SpecifyKind(new DateTime(date.Year, date.Month, date.Day),
                DateTimeKind.Utc));
            var grpcResponse = await _grpcClient.GetCurrentCurrencyOnDateAsync(new GetCurrentCurrencyOnDateRequest
            {
                CurrencyCode = currencyCode,
                Date = protoTimestamp
            });

            switch (grpcResponse.StatusCode)
            {
                case Grpc.Contracts.StatusCodes.NoError:
                    var value = decimal.Round(grpcResponse.Value, _options.Value.DecimalPlaces);
                    return value;
                case Grpc.Contracts.StatusCodes.UnknownCurrency:
                    throw new CurrencyNotFoundException(_options.Value.Currency);
                case Grpc.Contracts.StatusCodes.NoRequestsLeft:
                    throw new ApiRequestLimitException();
                default:
                    throw new Exception("Unknown status code returned from grpc service");
            }
        }
        public async Task<(string baseCurrency, bool canRequest)> GetRequestQuotas()
        {
            var grpcResponse = await _grpcClient.GetSettingsAsync(new Empty());
            return (grpcResponse.BaseCurrencyCode, grpcResponse.CanRequest);
        }
    }
}
