using Asp.Versioning;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);
var app = builder.Build();

app.MapDefaultEndpoints();

app.MapIdentityApiV1();

app.UseDefaultOpenApi();

app.Run();