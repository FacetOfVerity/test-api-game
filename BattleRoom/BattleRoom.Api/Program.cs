using BattleRoom.Api.Extensions;
using BattleRoom.Infrastructure.Database;
using BattleRoom.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .RegisterInfrastructureDependencies(builder.Configuration);

var app = builder.Build();

app.MigrateDatabase<BattleRoomLobbiesContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.RouteTemplate = "api/publisher/swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(a =>
    {
        a.SwaggerEndpoint("/api/publisher/swagger/v1/swagger.json", "Battle Room API");
        a.RoutePrefix = "api/help";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();