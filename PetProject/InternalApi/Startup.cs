using System.Text.Json.Serialization;
using Audit.Core;
using Audit.Http;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Data;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Grpc;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Middleware;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Repositories;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Services;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi
{
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
            services.AddControllers()
                .AddJsonOptions(
                options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                       new OpenApiInfo()
                       {
                           Title = "Internal Currency Api",
                           Version = "v1",
                           Description = "API для работы с курсами валют"
                       });
                c.IncludeXmlComments(Path.Combine(
                    AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml"),
                    includeControllerXmlComments: true
                    );
            });
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<ICurrencyAPI>(provider => provider.GetRequiredService<CurrencyService>());
            services.AddTransient<ICurrencySettingsApi>(provider => provider.GetRequiredService<CurrencyService>());
            services.AddTransient<IHealthCheckService>(provider => provider.GetRequiredService<CurrencyService>());
            services.AddTransient<ICachedCurrencyAPI, CachedCurrencyService>();
            services.AddTransient<RequestLoggingMiddleware>();
            var section = _configuration.GetSection(InternalApiSettings.CurrencyApiName);
            services.Configure<InternalApiSettings>(section);
            services
                .AddHttpClient<CurrencyService>((provider, client) =>
                {
                    using var scope = provider.CreateScope();
                    var apiSettings = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<InternalApiSettings>>().Value;
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
            services.AddGrpc();

            string connectionString = _configuration.GetConnectionString(nameof(InternalApiDbContext))!;
            services.AddDbContext<InternalApiDbContext>(options =>
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

            app.UseWhen(
                predicate: context => context.Connection.LocalPort == _configuration.GetValue<int>("GrpcPort"),
                configuration: grpcBuilder =>
                {
                    grpcBuilder.UseRouting();
                    grpcBuilder.UseEndpoints(endpoints => endpoints.MapGrpcService<GrpcCurrencyService>());
                });

            app.UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
