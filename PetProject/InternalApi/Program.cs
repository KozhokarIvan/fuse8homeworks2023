using Fuse8_ByteMinds.SummerSchool.InternalApi;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(
    webBuilder =>
    {
        webBuilder
        .UseKestrel((builderContext, options) =>
        {
            var grpcPort = builderContext.Configuration.GetValue<int>("GrpcPort");
            options.ConfigureEndpointDefaults(
                p =>
                {
                    p.Protocols = p.IPEndPoint!.Port == grpcPort
                    ? Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2
                    : Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
                });
        })
        .UseStartup<Startup>();
    })
    .Build();

await host.RunAsync();