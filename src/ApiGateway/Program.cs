using ApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddRouteConfigs();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{   
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.UseOcelot();

app.Run();