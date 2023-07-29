using System.Text.Json.Serialization;
using Audit.Core;
using Audit.Http;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Filters;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Middleware;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Options;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Services;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var serilogLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(_configuration)
            .CreateLogger();
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
            loggingBuilder.AddSerilog(logger: serilogLogger, dispose: true);
        });
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ApiExceptionFilter));
        })

            // Добавляем глобальные настройки для преобразования Json
            .AddJsonOptions(
                options =>
                {
                    // Добавляем конвертер для енама
                    // По умолчанию енам преобразуется в цифровое значение
                    // Этим конвертером задаем перевод в строковое занчение
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
        ;
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                   new OpenApiInfo()
                   {
                       Title = "Homework3",
                       Version = "v1",
                       Description = "API that allows you to get currency exchange rate"
                   });
            c.IncludeXmlComments(Path.Combine(
                AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml"),
                includeControllerXmlComments: true
                );
        });
        services.AddTransient<CurrencyService>();
        services.AddTransient<RequestLoggingMiddleware>();
        var section = _configuration.GetSection(CurrencyApiSettings.CurrencyApiName);
        services.Configure<CurrencyApiSettings>(section);
        services.AddHttpClient(
            CurrencyApiSettings.CurrencyApiName, config =>
            {
                config.BaseAddress =
                    new Uri(_configuration["CurrencyApi:Uri"]
                    ??
                    throw new NullReferenceException("You must set external currency API uri in configuration as 'CurrencyApi:Uri'"));
                config.DefaultRequestHeaders.Add("apikey", _configuration["CurrencyAPI:ApiKey"]
                    ??
                    throw new NullReferenceException("You must set external currency API key in configuration as 'CurrencyApi:ApiKey'"));
            })
            .AddAuditHandler(audit =>
                audit
                    .IncludeRequestBody()
                    .IncludeRequestHeaders()
                    .IncludeResponseBody()
                    .IncludeResponseHeaders()
                    .IncludeContentHeaders());
        Configuration.Setup()
            .UseSerilog(config =>
                config
                .Logger(ev => serilogLogger)
                .Message(auditEvent =>
                    {
                        if (auditEvent is AuditEventHttpClient auditEventHttpClient)
                        {
                            var contentBody = auditEventHttpClient.Action?.Response?.Content?.Body;
                            if (contentBody is string { Length: > 1000 } stringBody)
                            {
                                auditEventHttpClient.Action.Response.Content.Body = stringBody[..1000] + "<...>";
                            }
                        }
                        return auditEvent.ToJson();
                    }));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<RequestLoggingMiddleware>();
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting()
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}