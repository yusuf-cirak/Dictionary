using Dictionary.FavoriteService;
using Dictionary.FavoriteService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddTransient<FavoriteService>();
    })
    .Build();

await host.RunAsync();
