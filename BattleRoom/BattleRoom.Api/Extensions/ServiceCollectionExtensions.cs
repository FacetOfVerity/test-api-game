using BattleRoom.Api.Filters;

namespace BattleRoom.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<ExceptionFilter>();

        return services;
    }
}