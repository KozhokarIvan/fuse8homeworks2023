namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Middleware
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            HttpRequest request = context.Request;
            var requestGuid = Guid.NewGuid();
            _logger.LogInformation("Received {@method} {@endpoint} request id: {@guid}",
                request.Method,
                request.Path.Value,
                requestGuid);
            await next.Invoke(context);
            _logger.LogInformation("Processed {@method} {@endpoint} status: {@status} request id: {@guid}",
                request.Method,
                request.Path.Value,
                context.Response.StatusCode,
                requestGuid);
        }
    }
}
