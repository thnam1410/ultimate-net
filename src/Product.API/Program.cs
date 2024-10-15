var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);
var app = builder.Build();

app.MapDefaultEndpoints();

app.MapProductApiV1();

app.UseDefaultOpenApi();

app.Run();