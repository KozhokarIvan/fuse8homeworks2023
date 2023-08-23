using Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Exceptions;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Grpc.Contracts;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Grpc
{
    public class GrpcCurrencyService : GrpcCurrency.GrpcCurrencyBase
    {
        private readonly ICachedCurrencyAPI _cachedCurrencyAPI;
        private readonly ICurrencySettingsApi _currencySettingsApi;
        private readonly IOptionsSnapshot<CurrencyApiSettings> _options;
        public GrpcCurrencyService(
            ICachedCurrencyAPI cachedCurrencyApi,
            ICurrencySettingsApi currencySettingsApi,
            IOptionsSnapshot<CurrencyApiSettings> options)
        {
            _cachedCurrencyAPI = cachedCurrencyApi;
            _currencySettingsApi = currencySettingsApi;
            _options = options;
        }
        public override async Task<GetCurrencyResponse> GetCurrentCurrency(GetCurrentCurrencyRequest request, ServerCallContext context)
        {
            if (!Enum.TryParse<CurrencyCode>(request.CurrencyCode, true, out CurrencyCode currencyCode))
                return new GetCurrencyResponse { StatusCode = Contracts.StatusCodes.UnknownCurrency };
            try
            {
                var currentCurrency = await _cachedCurrencyAPI.GetCurrentCurrencyAsync(currencyCode, context.CancellationToken);
                return new GetCurrencyResponse
                {
                    StatusCode = Contracts.StatusCodes.NoError,
                    CurrencyCode = currentCurrency.CurrencyCode.ToString(),
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
            if (!Enum.TryParse<CurrencyCode>(request.CurrencyCode, true, out CurrencyCode currencyCode))
                return new GetCurrencyResponse { StatusCode = Contracts.StatusCodes.UnknownCurrency };
            try
            {
                var currencyOnDate = await _cachedCurrencyAPI.GetCurrencyOnDateAsync(
                currencyCode,
                DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(request.Date.Seconds).Date),
                context.CancellationToken);
                return new GetCurrencyResponse
                {
                    StatusCode= Contracts.StatusCodes.NoError,
                    CurrencyCode = currencyOnDate.CurrencyCode.ToString(),
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
            (int requestCount, int requestLimit) = await _currencySettingsApi.GetRequestQuotas();
            return new GetSettingsResponse
            {
                StatusCode = Contracts.StatusCodes.NoError,
                BaseCurrencyCode = _options.Value.BaseCurrency,
                CanRequest = requestLimit > requestCount,
            };
        }
    }
}
