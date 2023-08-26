using Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Requests;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Controllers
{
    /// <summary>
    /// Методы для работы с избранными парами валют, которые в будущем могут использоваться для получения курсов
    /// </summary>
    [Route("exchanges")]
    public class ExchangesController : ControllerBase
    {

        private readonly IFavoriteExchangesService _favoriteExchangesService;
        public ExchangesController(IFavoriteExchangesService favoriteExchangesService)
        {
            _favoriteExchangesService = favoriteExchangesService;
        }

        /// <summary>
        /// Возвращает избранную пару валют по названию
        /// </summary>
        /// <param name="name">Название избранного курса обмена</param>
        /// <response code="200">Возвращает при успешном получении избранной пары валют</response>
        /// <response code="404">Возвращает если избранной пары валют с таким названием не существует</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetFavoriteExchangeByNameResponse), StatusCodes.Status200OK)]
        [HttpGet("favorites/{name}")]
        public async Task<IActionResult> GetFavoriteExchangeByName(string name)
        {
            var cancellationToken = HttpContext.RequestAborted;
            var result = await _favoriteExchangesService.GetFavoriteExchangeByName(name, cancellationToken);
            if (result is null)
                NotFound("Избранного курса с таким именем не существует");
            var response = new GetFavoriteExchangeByNameResponse
            {
                FavoriteName = result!.Name,
                BaseCurrencyCode = result!.BaseCurrency,
                CurrencyCode = result!.Currency
            };
            return Ok(response);
        }

        /// <summary>
        /// Возвращает все избранные пары валют с их названиями
        /// </summary>
        /// <response code="200">Возвращает при успешном получении избранных пар валют</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetFavoriteExchangesResponse), StatusCodes.Status200OK)]
        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavoriteExchanges()
        {
            var cancellationToken = HttpContext.RequestAborted;
            var result = await _favoriteExchangesService.GetFavoriteExchanges(cancellationToken);
            var response = new GetFavoriteExchangesResponse
            {
                Favorites = result.Select(e
                => new FavoriteExchangeEntry(e.Name, e.Currency, e.BaseCurrency)).ToArray()
            };
            return Ok(response);
        }

        /// <summary>
        /// Создает новую избранную пару валют
        /// </summary>
        /// <response code="200">Возвращает при успешном создании избранной пары валют</response>
        /// <response code="409">Возвращает если избранная пара валют с такими параметрами уже существует (не уникальное имя или пара валют)</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [ProducesResponseType(typeof(GetFavoriteExchangeByNameResponse), StatusCodes.Status200OK)]
        [HttpPost("favorites")]
        public async Task<IActionResult> AddExchangeToFavorites(CreateFavoriteExchangeRequest request)
        {
            var cancellationToken = HttpContext.RequestAborted;
            var result = await _favoriteExchangesService.AddExchangeToFavorites(
                request.FavoriteName,
                request.Currency,
                request.BaseCurrency,
                cancellationToken);
            return result
                ? Ok($"Избранный курс '{request.FavoriteName}' был успешно добавлен")
                : Conflict("Избранный курс с такими параметрами уже существует");
        }

        /// <summary>
        /// Редактирует избранную пару валют по названию
        /// </summary>
        /// <param name="name">Название избранной пары валют</param>
        /// <param name="request">Параметры запроса</param>
        /// <response code="200">Возвращает при успешном редактировании избранной пары валют</response>
        /// <response code="404">Возвращает если избранной пары валют с таким названием не существует</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [HttpPut("favorites/{name}")]
        public async Task<IActionResult> EditFavoriteExchange(string name, EditFavoriteExchangeRequest request)
        {
            var cancellationToken = HttpContext.RequestAborted;
            var result = await _favoriteExchangesService.EditFavoriteExchange(
                name,
                request.Currency,
                request.BaseCurrency,
                cancellationToken);
            if (result is null)
                return NotFound($"Избранного курса с названием '{name}' не существует");
            return result.Value
                ? Ok($"Избранный курс '{name}' был успешно редактирован")
                : Conflict($"Избранный курс '{request.Currency}' к '{request.BaseCurrency}' уже существует");
        }

        /// <summary>
        /// Удаляет избранную пару валют по названию
        /// </summary>
        /// <param name="name">Название избранной пары валют</param>
        /// <response code="200">Возвращает при успешном удалении избранной пары валют</response>
        /// <response code="404">Возвращает если избранной пары валют с таким названием не существует</response>
        /// <response code="500">Возвращает при возникновении необработанной ошибки</response>
        [HttpDelete("favorites/{name}")]
        public async Task<IActionResult> DeleteFavoriteExchangeByName(string name)
        {
            var cancellationToken = HttpContext.RequestAborted;
            var result = await _favoriteExchangesService.DeleteFavoriteExchangeByName(name, cancellationToken);
            return result
            ? Ok($"Избранный курс с названием '{name}' был успешно удален")
            : NotFound($"Избранного курса с названием '{name}' не существует");
        }
    }
}
