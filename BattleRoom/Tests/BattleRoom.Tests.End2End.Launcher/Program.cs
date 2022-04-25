using BattleRoom.Tests.End2End.Launcher;
using BattleRoom.Tests.End2End.Launcher.Http;
using Microsoft.AspNetCore.SignalR.Client;
using Refit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddRefitClient<IBattleRoomHttpClient>()
            .ConfigureHttpClient((_, client) =>
                client.BaseAddress = new Uri(hostContext.Configuration.GetValue<string>("BattleRoomApiUrl")));

        services.AddScoped(_ => new HubConnectionBuilder()
            .WithUrl(new Uri(hostContext.Configuration.GetValue<string>("LobbiesHubUrl")))
            .Build());
        
        services.AddLogging(config => config.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "hh:mm:ss ";
        }));

    })
    .Build();

await host.RunAsync();