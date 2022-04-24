using BattleRoom.Application.Abstractions;
using BattleRoom.Application.Features;
using BattleRoom.Application.Mapping;
using BattleRoom.Application.Utils;
using BattleRoom.Client.Abstractions;
using BattleRoom.Domain.Abstractions;
using BattleRoom.Infrastructure.Database;
using BattleRoom.Infrastructure.SignalR;
using BattleRoom.Infrastructure.SignalR.Hubs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace BattleRoom.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        //Ef
        services.AddDbContext<BattleRoomLobbiesContext>((Action<DbContextOptionsBuilder>) (options =>
            options.UseNpgsql(configuration.GetSection("DbConnection").Get<NpgsqlConnectionStringBuilder>()
                .ConnectionString)));
        services.AddScoped<ILobbiesContext, BattleRoomLobbiesContext>();
        services.AddScoped<IPlayersContext, BattleRoomLobbiesContext>();
        
        // SignalR
        services.AddSignalR();
        services.AddScoped<ISubscribersNotifier<ILobbyActionsHandler>,
            SignalRNotifier<LobbiesEventsHub, ILobbyActionsHandler>>();
        services.AddScoped<IGameActionsPublisher, GameActionsPublisher>();
        
        //Other
        services.AddMediatR(typeof(OpenTheLobbyCommand).Assembly);
        
        services.AddLogging(config => config.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "hh:mm:ss ";
        }));

        services.AddAutoMapper(a => a.AddProfile(typeof(MappingProfile)));

        return services;
    }
}