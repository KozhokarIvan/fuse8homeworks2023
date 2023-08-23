using Fuse8_ByteMinds.SummerSchool.InternalApi;
using Microsoft.AspNetCore;

var webHost = WebHost
    .CreateDefaultBuilder(args)
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
    .UseStartup<Startup>()
    .Build();

await webHost.RunAsync();