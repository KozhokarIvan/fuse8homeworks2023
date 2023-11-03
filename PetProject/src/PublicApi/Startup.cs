using System.Text.Json.Serialization;
using Audit.Core;
using Audit.Http;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Data;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Repositories;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Filters;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Grpc.Contracts;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Middleware;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Options;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
                       Title = "Public Currency Api",
                       Version = "v1",
                       Description = "API для работы с курсами валют"
                   });
            c.IncludeXmlComments(Path.Combine(
                AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml"),
                includeControllerXmlComments: true
                );
        });
        services.AddScoped<IFavoriteExchangesRepository, FavoriteExchangesRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();
        services.AddTransient<ICurrencyService>(provider => provider.GetRequiredService<CurrencyService>());
        services.AddTransient<IHealthCheckService>(provider => provider.GetRequiredService<CurrencyService>());
        services.AddTransient<IFavoriteExchangesService, FavoriteExchangesService>();
        services.AddTransient<ISettingsService, SettingsService>();
        services.AddTransient<RequestLoggingMiddleware>();

        var section = _configuration.GetSection(PublicApiSettings.CurrencyApiName);
        services.Configure<PublicApiSettings>(section);
        services
            .AddHttpClient<CurrencyService>((provider, client) =>
                {
                    using var scope = provider.CreateScope();
                    var apiSettings = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<PublicApiSettings>>().Value;
                    client.BaseAddress = new Uri(apiSettings.Uri);
                    client.DefaultRequestHeaders.Add("apikey", apiSettings.ApiKey);
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
                                auditEventHttpClient.Action!.Response.Content.Body = stringBody[..1000] + "<...>";
                            }
                        }
                        return auditEvent.ToJson();
                    }));
        services
            .AddGrpcClient<GrpcCurrency.GrpcCurrencyClient>(o =>
                {
                    o.Address = new Uri(_configuration.GetValue<string>("GrpcUri")!);
                })
            .AddAuditHandler(audit => audit.IncludeRequestBody());

        string connectionString = _configuration.GetConnectionString(nameof(PublicApiDbContext))!;
        services.AddDbContext<PublicApiDbContext>(options =>
        {
            options
            .UseNpgsql(
                connectionString: connectionString,
                npgsqlOptionsAction: sqlOptionsBuilder =>
                {
                    sqlOptionsBuilder.EnableRetryOnFailure();
                })
            .UseSnakeCaseNamingConvention();
        });
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