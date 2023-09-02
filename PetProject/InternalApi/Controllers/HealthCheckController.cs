using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Controllers
{
    /// <summary>
    /// Методы для определения работоспособности API
    /// </summary>
    [Route("health")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IHealthCheckService _healthCheckAPI;
        public HealthCheckController(IHealthCheckService healthCheckAPI)
        {
            _healthCheckAPI = healthCheckAPI;
        }
        /// <summary>
        /// Проверяет, работает ли внешний API https://currencyapi.com/
        /// </summary>
        /// <response code="200">Возвращает, если API работает</response>
        /// <response code="503">Возвращает, при сбое в работе API</response>
        [HttpGet]
        public async Task<IActionResult> CheckHealth(CancellationToken cancellationToken)
        => await _healthCheckAPI.CheckHealth(cancellationToken)
                ? Ok()
                : new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
    }
}
