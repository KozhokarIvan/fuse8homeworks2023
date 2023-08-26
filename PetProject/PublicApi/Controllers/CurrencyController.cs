using Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Requests;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Controllers
{
    /// <summary>
    /// Методы для работы с курсом валют
    /// </summary>
    [Route("currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly ISettingsService _settingsService;
        public CurrencyController(
            ICurrencyService currencyService,
            ISettingsService settingsService)
        {
            _currencyService = currencyService;
            _settingsService = settingsService;
        }

        /// <summary>
        /// Получает курс валюты относительно базового. Базовый курс, курс валюты и точность округления извлекаются из параметров по умолчанию
        /// </summary>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetCurrencyResponse), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetDefaultCurrency()
        {
            var cancellationToken = HttpContext.RequestAborted;
            (string currencyCode, decimal value) = await _currencyService.GetDefaultCurrency(cancellationToken);
            var response = new GetCurrencyResponse
            {
                Code = currencyCode,
                Value = value
            };
            return Ok(response);
        }

        /// <summary>
        /// Получает курс валюты относительно базового. Базовый курс и точность округления извлекаются из параметров по умолчанию
        /// </summary>
        /// <param name="currency">Валюта для которой нужно получить курс</param>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="404">Возвращает при попытке получить курс для неизвестной валюты</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetCurrencyResponse), StatusCodes.Status200OK)]
        [HttpGet("{currency}")]
        public async Task<IActionResult> GetCurrencyByCode(string currency)
        {
            var cancellationToken = HttpContext.RequestAborted;
            (string code, decimal value) = await _currencyService.GetCurrencyByCode(currency, cancellationToken);
            var response = new GetCurrencyResponse
            {
                Code = code,
                Value = value
            };
            return Ok(response);
        }

        /// <summary>
        /// Получает курс валюты относительно базового для заданной даты. Базовый курс и точность округления извлекаются из параметров по умолчанию
        /// </summary>
        /// <param name="currency">Валюта для которой нужно получить курс</param>
        /// <param name="date">Дата для которой требуется получить курс</param>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="400">Возвращает при запросе курса с форматом даты отличным от 'yyyy-MM-dd'</response>
        /// <response code="404">Возвращает при попытке получить курс для неизвестной валюты</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetCurrencyOnDateResponse), StatusCodes.Status200OK)]
        [HttpGet("{currency}/{date}")]
        public async Task<IActionResult> GetCurrencyOnDate(string currency, string date)
        {
            bool isDateValid = DateOnly.TryParseExact(date, "yyyy-MM-dd", out var parsedDate);
            if (!isDateValid)
                return BadRequest($"Ожидалась дата формата: 'yyyy-MM-dd', получена: '{date}'");
            var cancellationToken = HttpContext.RequestAborted;
            decimal value = await _currencyService.GetCurrencyOnDate(currency, parsedDate, cancellationToken);
            var response = new GetCurrencyOnDateResponse
            {
                Date = parsedDate,
                Code = currency,
                Value = value
            };
            return Ok(response);
        }
        /// <summary>
        /// Получает избранный курс по названию. Точность округления извлекается из параметров по умолчанию
        /// </summary>
        /// <param name="name">Название избранного курса обмена</param>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="404">Возвращает при попытке получить курс для неизвестного названия избранного</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetFavoriteExchangeRateResponse), StatusCodes.Status200OK)]
        [HttpGet("favorites/{name}")]
        public async Task<IActionResult> GetFavoriteExchangeRateByName(string name)
        {
            var cancellationToken = HttpContext.RequestAborted;
            var exchangeRate = await _currencyService.GetFavoriteCurrency(name, cancellationToken);
            return exchangeRate is not null
                ? Ok(new GetFavoriteExchangeRateResponse { ExchangeRate = exchangeRate.Value })
                : NotFound($"Избранного курса с именем {name} не существует");
        }

        /// <summary>
        /// Получает избранный курс по названию на выбранную дату. Точность округления извлекается из параметров по умолчанию
        /// </summary>
        /// <param name="name">Название избранного курса обмена</param>
        /// <param name="date">Дата для которой требуется получить курс</param>
        /// <response code="200">Возвращает при успешном получении курса</response>
        /// <response code="404">Возвращает при попытке получить курс для неизвестного названия избранного</response>
        /// <response code="429">Возвращает если при попытке получить курс у вас превышен лимит запросов</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetFavoriteExchangeRateResponse), StatusCodes.Status200OK)]
        [HttpGet("favorites/{name}/{date}")]
        public async Task<IActionResult> GetFavoriteExchangeRateByName(string name, string date)
        {
            bool isDateValid = DateOnly.TryParseExact(date, "yyyy-MM-dd", out var parsedDate);
            if (!isDateValid)
                return BadRequest($"Ожидалась дата формата: 'yyyy-MM-dd', получена: '{date}'");
            var cancellationToken = HttpContext.RequestAborted;
            var exchangeRate = await _currencyService.GetFavoriteCurrencyOnDate(name, parsedDate, cancellationToken);
            return exchangeRate is not null
                ? Ok(new GetFavoriteExchangeRateResponse { ExchangeRate = exchangeRate.Value }) :
                NotFound($"Избранного курса с именем {name} не существует");
        }

        /// <summary>
        /// Получает настройки 
        /// </summary>
        /// <response code="200">Возвращает при успешном получении настроек</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetSettingsResponse), StatusCodes.Status200OK)]
        [HttpGet("/settings")]
        public async Task<IActionResult> GetSettings()
        {
            var cancellationToken = HttpContext.RequestAborted;
            (string baseCurrency, bool canRequest) = await _currencyService.GetRequestQuotas(cancellationToken);
            (string defaultCurrency, int decimalPlaces) = await _settingsService.GetSettings(cancellationToken);
            var settings = new GetSettingsResponse()
            {
                DefaultCurrency = defaultCurrency,
                BaseCurrency = baseCurrency,
                NewRequestsAvailable = canRequest,
                CurrencyRoundCount = decimalPlaces
            };
            return Ok(settings);
        }

        /// <summary>
        /// Изменяет значение для валюты по умолчанию
        /// </summary>
        /// <response code="200">Возвращает при успешном изменении валюты по умолчанию</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [HttpPost("/settings/currency")]
        public async Task<IActionResult> SetDefaultCurrency(SetDefaultCurrencyRequest request)
        {
            var cancellationToken = HttpContext.RequestAborted;
            await _settingsService.SetDefaultCurrency(request.CurrencyCode.ToString(), cancellationToken);
            return Ok("Валюта по умолчанию успешно изменена");
        }

        /// <summary>
        /// Изменяет значение точности округления
        /// </summary>
        /// <response code="200">Возвращает при успешном изменении точности округления</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [HttpPost("/settings/decimal-places")]
        public async Task<IActionResult> SetDecimalPlaces(SetDecimalPlacesRequest request)
        {
            var cancellationToken = HttpContext.RequestAborted;
            await _settingsService.SetDecimalPlaces(request.DecimalPlaces, cancellationToken);
            return Ok("Количество десятичных разрядов для округления успешно изменено");
        }
    }
}
