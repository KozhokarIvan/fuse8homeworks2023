using Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.Requests;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.Responses;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Exceptions;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Controllers
{
    /// <summary>
    /// Методы для работы с внешним API https://currencyapi.com/
    /// </summary>
    [Route("currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICachedCurrencyAPI _cachedCurrencyAPI;
        private readonly ICurrencySettingsApi _currencySettingsApi;
        private readonly ICacheTasksService _cacheTaskService;
        public CurrencyController(
            ICachedCurrencyAPI cachedCurrencyAPI,
            ICurrencySettingsApi currencySettingsApi,
            ICacheTasksService cacheTaskService)
        {
            _cachedCurrencyAPI = cachedCurrencyAPI;
            _currencySettingsApi = currencySettingsApi;
            _cacheTaskService = cacheTaskService;
        }
        /// <summary>
        /// Возвращает курс выбранной валюты относительно базового, актуальный на данный момент
        /// </summary>
        /// <param name="request">Модель запроса валюты</param>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetCurrencyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrencyByCode(GetCurrencyRequest request)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            try
            {
                var result = await _cachedCurrencyAPI.GetCurrentCurrencyAsync(request.CurrencyType.ToString(), cancellationToken);
                var response = new GetCurrencyResponse
                {
                    Code = result.Code,
                    Value = result.Value
                };
                return Ok(response);
            }
            catch (ApiRequestLimitException)
            {
                return new StatusCodeResult(StatusCodes.Status429TooManyRequests);
            }
        }

        /// <summary>
        /// Возвращает курс выбранной валюты относительно базового, актуальный на данный момент
        /// </summary>
        /// <param name="request">Модель запроса валюты</param>
        /// <param name="date">Дата, на момент которой нужно определить курс валюты</param>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="400">Возвращает если дата указана в формате отличном от yyyy-MM-dd (например, 2022-02-22)</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [HttpGet("{date}")]
        [ProducesResponseType(typeof(GetCurrencyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrencyOnDate(GetCurrencyRequest request, string date)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            bool isDateValid = DateOnly.TryParseExact(date, "yyyy-MM-dd", out var parsedDate);
            if (!isDateValid)
                return BadRequest($"Ожидалась дата формата: 'yyyy-MM-dd', получена: '{date}'");
            try
            {
                var result = await _cachedCurrencyAPI.GetCurrencyOnDateAsync(request.CurrencyType.ToString(), parsedDate, cancellationToken);
                var response = new GetCurrencyResponse { Code = result.Code, Value = result.Value };
                return Ok(response);
            }
            catch (ApiRequestLimitException)
            {
                return new StatusCodeResult(StatusCodes.Status429TooManyRequests);
            }
        }

        /// <summary>
        /// Возвращает настройки API
        /// </summary>
        /// <response code="200">Возвращает при успешном получении настроек</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [HttpGet("settings")]
        [ProducesResponseType(typeof(GetSettingsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSettings()
        {
            var cancellationToken = HttpContext.RequestAborted;
            (int requestCount, int requestLimit) = await _currencySettingsApi.GetRequestQuotas(cancellationToken);
            var response = new GetSettingsResponse
            {
                RequestsAvailable = requestCount <= requestLimit,
                BaseCurrency = await _currencySettingsApi.GetBaseCurrency(cancellationToken)
            };
            return Ok(response);
        }

        [HttpPost("cache")]
        public async Task<IActionResult> SetNewBaseCurrency(SetNewBaseCurrencyRequest request)
        {
            var cancellationToken = HttpContext.RequestAborted;
            var taskId = await _cacheTaskService.AddTaskAsync(request.NewBaseCurrency.ToString(), cancellationToken);
            return Accepted(taskId);
        }
    }
}
