using Dictionary.VoteService;
using Dictionary.VoteService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddTransient<VoteService>();
    })
    .Build();

await host.RunAsync();
