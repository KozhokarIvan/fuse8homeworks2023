using Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Options;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Controllers
{
    /// <summary>
    /// Методы для работы с PublicApi
    /// </summary>
    [Route("currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly IOptionsSnapshot<CurrencyApiSettings> _currencyOptions;
        private readonly CurrencyService _currencyService;
        public CurrencyController(
            IOptionsSnapshot<CurrencyApiSettings> currencyOptions,
            CurrencyService currencyService)
        {
            _currencyOptions = currencyOptions;
            _currencyService = currencyService;
        }

        /// <summary>
        /// Получает курс валюты относительно базового. Базовый курс, курс валюты и точность округления извлекаются из параметров по умолчанию
        /// </summary>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(CurrencyResponse), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetDefaultCurrency()
        {
            (string currencyCode, decimal value) = await _currencyService.GetDefaultCurrency();
            var response = new CurrencyResponse
            {
                Code = currencyCode,
                Value = value
            };
            return Ok(response);
        }

        /// <summary>
        /// Получает курс валюты относительно базового. Базовый курс и точность округления извлекаются из параметров по умолчанию
        /// </summary>
        /// <param name="currencyCode">Валюта для которой нужно получить курс</param>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="404">Возвращает при попытке получить курс для неизвестной валюты</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(CurrencyResponse), StatusCodes.Status200OK)]
        [HttpGet("{currencyCode}")]
        public async Task<IActionResult> GetCurrencyByCode(string currencyCode)
        {
            (string code, decimal value) = await _currencyService.GetCurrencyByCode(currencyCode);
            var response = new CurrencyResponse
            {
                Code = code,
                Value = value
            };
            return Ok(response);
        }

        /// <summary>
        /// Получает курс валюты относительно базового для заданной даты. Базовый курс и точность округления извлекаются из параметров по умолчанию
        /// </summary>
        /// <param name="currencyCode">Валюта для которой нужно получить курс</param>
        /// <param name="date">Дата для которой требуется получить курс</param>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="400">Возвращает при запросе курса с форматом даты отличным от 'yyyy-MM-dd'</response>
        /// <response code="404">Возвращает при попытке получить курс для неизвестной валюты</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(CurrencyWithDateResponse), StatusCodes.Status200OK)]
        [HttpGet("{currencyCode}/{date}")]
        public async Task<IActionResult> GetCurrencyByCodeAndDate(string currencyCode, string date)
        {
            bool isDateValid = DateOnly.TryParseExact(date, "yyyy-MM-dd", out var parsedDate);
            if (!isDateValid)
                return BadRequest($"Expected date format: 'yyyy-MM-dd', received date: '{date}'");
            decimal value = await _currencyService.GetCurrencyByCodeAndDate(currencyCode, parsedDate);
            var response = new CurrencyWithDateResponse
            {
                Date = parsedDate,
                Code = currencyCode,
                Value = value
            };
            return Ok(response);
        }

        /// <summary>
        /// Получает настройки 
        /// </summary>
        /// <response code="200">Возвращает при успешном получении настроек</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(SettingsResponse), StatusCodes.Status200OK)]
        [HttpGet("/settings")]
        public async Task<IActionResult> GetSettings()
        {
            (int requestCount, int requestLimit) = await _currencyService.GetRequestQuotas();
            var settings = new SettingsResponse()
            {
                DefaultCurrency = _currencyOptions.Value.Currency,
                BaseCurrency = _currencyOptions.Value.BaseCurrency,
                CurrencyRoundCount = _currencyOptions.Value.DecimalPlaces,
                RequestCount = requestCount,
                RequestLimit = requestLimit
            };
            return Ok(settings);
        }
    }
}
