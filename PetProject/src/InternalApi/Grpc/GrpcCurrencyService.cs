using Fuse8_ByteMinds.SummerSchool.InternalApi.Exceptions;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Grpc.Contracts;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services;
using Grpc.Core;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Grpc
{
    public class GrpcCurrencyService : GrpcCurrency.GrpcCurrencyBase
    {
        private readonly ICachedCurrencyAPI _cachedCurrencyAPI;
        private readonly ICurrencySettingsApi _currencySettingsApi;
        public GrpcCurrencyService(
            ICachedCurrencyAPI cachedCurrencyApi,
            ICurrencySettingsApi currencySettingsApi)
        {
            _cachedCurrencyAPI = cachedCurrencyApi;
            _currencySettingsApi = currencySettingsApi;
        }
        public override async Task<GetCurrencyResponse> GetCurrentCurrency(GetCurrentCurrencyRequest request, ServerCallContext context)
        {
            try
            {
                var currentCurrency = await _cachedCurrencyAPI.GetCurrentCurrencyAsync(
                    request.CurrencyCode,
                    context.CancellationToken);
                return new GetCurrencyResponse
                {
                    StatusCode = Contracts.StatusCodes.NoError,
                    CurrencyCode = currentCurrency.Code,
                    Value = currentCurrency.Value
                };
            }
            catch (ApiRequestLimitException)
            {
                return new GetCurrencyResponse { StatusCode = Contracts.StatusCodes.NoRequestsLeft };
            }
            catch (CurrencyNotFoundException)
            {
                return new GetCurrencyResponse { StatusCode = Contracts.StatusCodes.UnknownCurrency };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override async Task<GetCurrencyResponse> GetCurrentCurrencyOnDate(GetCurrentCurrencyOnDateRequest request, ServerCallContext context)
        {
            try
            {
                var currencyOnDate = await _cachedCurrencyAPI.GetCurrencyOnDateAsync(
                request.CurrencyCode,
                DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(request.Date.Seconds).Date),
                context.CancellationToken);
                return new GetCurrencyResponse
                {
                    StatusCode = Contracts.StatusCodes.NoError,
                    CurrencyCode = currencyOnDate.Code,
                    Value = currencyOnDate.Value
                };
            }
            catch (ApiRequestLimitException)
            {
                return new GetCurrencyResponse { StatusCode = Contracts.StatusCodes.NoRequestsLeft };
            }
            catch (CurrencyNotFoundException)
            {
                return new GetCurrencyResponse { StatusCode = Contracts.StatusCodes.UnknownCurrency };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override async Task<GetSettingsResponse> GetSettings(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            (int requestCount, int requestLimit) = await _currencySettingsApi.GetRequestQuotas(context.CancellationToken);
            return new GetSettingsResponse
            {
                StatusCode = Contracts.StatusCodes.NoError,
                BaseCurrencyCode = await _currencySettingsApi.GetBaseCurrency(context.CancellationToken),
                CanRequest = requestLimit > requestCount,
            };
        }
        public override async Task<GetFavoriteCurrencyResponse> GetFavoriteCurrency(GetFavoriteCurrencyRequest request, ServerCallContext context)
        {
            try
            {
                var exchangeRate = await _cachedCurrencyAPI
                    .GetFavoriteCurrency(
                    request.FavoriteCurrencyBase,
                    request.FavoriteCurrency,
                    context.CancellationToken);
                return new GetFavoriteCurrencyResponse
                {
                    StatusCode = Contracts.StatusCodes.NoError,
                    Value = exchangeRate
                };
            }
            catch (ApiRequestLimitException)
            {
                return new GetFavoriteCurrencyResponse { StatusCode = Contracts.StatusCodes.NoRequestsLeft };
            }
            catch (CurrencyNotFoundException)
            {
                return new GetFavoriteCurrencyResponse { StatusCode = Contracts.StatusCodes.UnknownCurrency };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override async Task<GetFavoriteCurrencyResponse> GetFavoriteCurrencyOnDate(GetFavoriteCurrencyOnDateRequest request, ServerCallContext context)
        {
            try
            {
                var dateOnly = DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(request.Date.Seconds).Date);
                var exchangeRate = await _cachedCurrencyAPI
                    .GetFavoriteCurrencyOnDate(
                    request.FavoriteCurrencyBase,
                    request.FavoriteCurrency,
                    dateOnly,
                    context.CancellationToken);
                return new GetFavoriteCurrencyResponse
                {
                    StatusCode = Contracts.StatusCodes.NoError,
                    Value = exchangeRate
                };
            }
            catch (ApiRequestLimitException)
            {
                return new GetFavoriteCurrencyResponse { StatusCode = Contracts.StatusCodes.NoRequestsLeft };
            }
            catch (CurrencyNotFoundException)
            {
                return new GetFavoriteCurrencyResponse { StatusCode = Contracts.StatusCodes.UnknownCurrency };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
