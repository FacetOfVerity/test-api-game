using BattleRoom.Application.Abstractions;
using BattleRoom.Application.Features;
using BattleRoom.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        
        //Other
        services.AddMediatR(typeof(CreateLobbyCommand).Assembly);

        return services;
    }
}