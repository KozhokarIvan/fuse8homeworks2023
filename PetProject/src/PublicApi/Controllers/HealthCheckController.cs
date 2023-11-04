using Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using static Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses.HealthCheckResponse;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Controllers;

/// <summary>
/// Методы для проверки работоспособности PublicApi
/// </summary>
[Route("health")]
public class HealthCheckController : ControllerBase
{
    private readonly IHealthCheckService _healthCheckService;
    public HealthCheckController(IHealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    /// <summary>
    /// Проверить что API работает
    /// </summary>
    /// <param name="checkExternalApi">Необходимо проверить работоспособность внешнего API. Если FALSE или NULL - проверяется работоспособность только текущего API</param>
    /// <response code="200">
    /// Возвращает если удалось получить доступ к API
    /// </response>
    /// <response code="400">
    /// Возвращает если удалось не удалось получить доступ к API
    /// </response>
    [HttpGet]
    public async Task<HealthCheckResponse> Check(bool? checkExternalApi)
    {
        var checkedOn = DateTimeOffset.Now;
        CheckStatus status;
        if (checkExternalApi == true)
            status = await _healthCheckService.CheckHealth() ? CheckStatus.Ok : CheckStatus.Failed;
        else
            status = CheckStatus.Ok;
        var response = new HealthCheckResponse { CheckedOn = checkedOn, Status = status };
        return response;
    }
}