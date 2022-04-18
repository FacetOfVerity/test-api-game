var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
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