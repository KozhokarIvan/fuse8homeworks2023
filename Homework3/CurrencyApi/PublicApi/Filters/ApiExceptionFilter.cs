using Fuse8_ByteMinds.SummerSchool.PublicApi.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;
        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CurrencyNotFoundException)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (context.Exception is ApiRequestLimitException)
            {
                _logger.LogError(message: "Exceeded external api request limit", exception: context.Exception);
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            }
            else
            {
                _logger.LogError(message: "Exception occured", exception: context.Exception);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}
