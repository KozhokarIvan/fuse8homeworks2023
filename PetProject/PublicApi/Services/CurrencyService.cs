using System.Net;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Exceptions;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Grpc.Contracts;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services;
using Google.Protobuf.WellKnownTypes;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Services
{
    public class CurrencyService : ICurrencyService, IHealthCheckService
    {
        private readonly HttpClient _httpClient;
        private readonly GrpcCurrency.GrpcCurrencyClient _grpcClient;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IFavoriteExchangesRepository _favoriteExchangesRepository;
        public CurrencyService(
            HttpClient httpClient,
            GrpcCurrency.GrpcCurrencyClient grpcClient,
            ISettingsRepository settingsRepository,
            IFavoriteExchangesRepository favoriteExchangesRepository)
        {
            _httpClient = httpClient;
            _grpcClient = grpcClient;
            _settingsRepository = settingsRepository;
            _favoriteExchangesRepository = favoriteExchangesRepository;
        }
        public async Task<bool> CheckHealth()
        {
            var response = await _httpClient.GetAsync("status");
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<(string code, decimal value)> GetDefaultCurrency(CancellationToken cancellationToken)
        {
            string defaultCurrency = await _settingsRepository.GetDefaultCurrency(cancellationToken);
            var grpcResponse = await _grpcClient.GetCurrentCurrencyAsync(new GetCurrentCurrencyRequest()
            {
                CurrencyCode = defaultCurrency
            }, cancellationToken: cancellationToken);
            switch (grpcResponse.StatusCode)
            {
                case Grpc.Contracts.StatusCodes.NoError:
                    int decimalPlaces = await _settingsRepository.GetDecimalPlaces(cancellationToken);
                    var value = decimal.Round(grpcResponse.Value, decimalPlaces);
                    return (grpcResponse.CurrencyCode, value);
                case Grpc.Contracts.StatusCodes.UnknownCurrency:
                    throw new CurrencyNotFoundException(defaultCurrency);
                case Grpc.Contracts.StatusCodes.NoRequestsLeft:
                    throw new ApiRequestLimitException();
                default:
                    throw new Exception("Unknown status code returned from grpc service");
            }
        }
        public async Task<(string code, decimal value)> GetCurrencyByCode(string currencyCode, CancellationToken cancellationToken)
        {
            var grpcResponse = await _grpcClient.GetCurrentCurrencyAsync(new GetCurrentCurrencyRequest()
            {
                CurrencyCode = currencyCode
            }, cancellationToken: cancellationToken);
            switch (grpcResponse.StatusCode)
            {
                case Grpc.Contracts.StatusCodes.NoError:
                    int decimalPlaces = await _settingsRepository.GetDecimalPlaces(cancellationToken);
                    var value = decimal.Round(grpcResponse.Value, decimalPlaces);
                    return (grpcResponse.CurrencyCode, value);
                case Grpc.Contracts.StatusCodes.UnknownCurrency:
                    throw new CurrencyNotFoundException(currencyCode);
                case Grpc.Contracts.StatusCodes.NoRequestsLeft:
                    throw new ApiRequestLimitException();
                default:
                    throw new Exception("Unknown status code returned from grpc service");
            }
        }
        public async Task<decimal> GetCurrencyOnDate(string currencyCode, DateOnly date, CancellationToken cancellationToken)
        {
            Timestamp protoTimestamp = Timestamp.FromDateTime(
                DateTime.SpecifyKind(new DateTime(date.Year, date.Month, date.Day),
                DateTimeKind.Utc));
            var grpcResponse = await _grpcClient.GetCurrentCurrencyOnDateAsync(new GetCurrentCurrencyOnDateRequest
            {
                CurrencyCode = currencyCode,
                Date = protoTimestamp
            }, cancellationToken: cancellationToken);
            switch (grpcResponse.StatusCode)
            {
                case Grpc.Contracts.StatusCodes.NoError:
                    int decimalPlaces = await _settingsRepository.GetDecimalPlaces(cancellationToken);
                    var value = decimal.Round(grpcResponse.Value, decimalPlaces);
                    return value;
                case Grpc.Contracts.StatusCodes.UnknownCurrency:
                    throw new CurrencyNotFoundException(currencyCode);
                case Grpc.Contracts.StatusCodes.NoRequestsLeft:
                    throw new ApiRequestLimitException();
                default:
                    throw new Exception("Unknown status code returned from grpc service");
            }
        }
        public async Task<decimal?> GetFavoriteCurrency(string favoriteExchangeName, CancellationToken cancellationToken)
        {
            var favoriteCurrency = await _favoriteExchangesRepository.GetFavoriteExchangeByName(favoriteExchangeName, cancellationToken);
            if (favoriteCurrency is null) return null;
            var grpcResponse = await _grpcClient.GetFavoriteCurrencyAsync(new GetFavoriteCurrencyRequest
            {
                FavoriteCurrency = favoriteCurrency.Currency,
                FavoriteCurrencyBase = favoriteCurrency.BaseCurrency
            }, cancellationToken: cancellationToken);
            switch (grpcResponse.StatusCode)
            {
                case Grpc.Contracts.StatusCodes.NoError:
                    int decimalPlaces = await _settingsRepository.GetDecimalPlaces(cancellationToken);
                    var value = decimal.Round(grpcResponse.Value, decimalPlaces);
                    return value;
                case Grpc.Contracts.StatusCodes.UnknownCurrency:
                    throw new CurrencyNotFoundException($"{favoriteCurrency.Currency} or {favoriteCurrency.BaseCurrency}");
                case Grpc.Contracts.StatusCodes.NoRequestsLeft:
                    throw new ApiRequestLimitException();
                default:
                    throw new Exception("Unknown status code returned from grpc service");
            }
        }
        public async Task<decimal?> GetFavoriteCurrencyOnDate(string favoriteExchangeName, DateOnly date, CancellationToken cancellationToken)
        {
            Timestamp protoTimestamp = Timestamp.FromDateTime(
            DateTime.SpecifyKind(new DateTime(date.Year, date.Month, date.Day),
                DateTimeKind.Utc));
            var favoriteCurrency = await _favoriteExchangesRepository.GetFavoriteExchangeByName(favoriteExchangeName, cancellationToken);
            if (favoriteCurrency is null) return null;
            var grpcResponse = await _grpcClient.GetFavoriteCurrencyOnDateAsync(new GetFavoriteCurrencyOnDateRequest
            {
                FavoriteCurrency = favoriteCurrency.Currency,
                FavoriteCurrencyBase = favoriteCurrency.BaseCurrency,
                Date = protoTimestamp
            }, cancellationToken: cancellationToken);
            switch (grpcResponse.StatusCode)
            {
                case Grpc.Contracts.StatusCodes.NoError:
                    int decimalPlaces = await _settingsRepository.GetDecimalPlaces(cancellationToken);
                    var value = decimal.Round(grpcResponse.Value, decimalPlaces);
                    return value;
                case Grpc.Contracts.StatusCodes.UnknownCurrency:
                    throw new CurrencyNotFoundException($"{favoriteCurrency.Currency} or {favoriteCurrency.BaseCurrency}");
                case Grpc.Contracts.StatusCodes.NoRequestsLeft:
                    throw new ApiRequestLimitException();
                default:
                    throw new Exception("Unknown status code returned from grpc service");
            }
        }
        public async Task<(string baseCurrency, bool canRequest)> GetRequestQuotas(CancellationToken cancellationToken)
        {
            var grpcResponse = await _grpcClient.GetSettingsAsync(new Empty(), cancellationToken: cancellationToken);
            return (grpcResponse.BaseCurrencyCode, grpcResponse.CanRequest);
        }
    }
}
