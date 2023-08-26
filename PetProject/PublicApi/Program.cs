using Fuse8_ByteMinds.SummerSchool.PublicApi;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webHost =>
    {
        webHost.UseStartup<Startup>();
    })
    .Build();

await host.RunAsync();