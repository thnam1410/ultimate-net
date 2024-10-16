using Identity.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDefaultAuthentication();

builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();
builder.AddDefaultOpenApi(withApiVersioning);

builder.Services.AddSingleton<JwtTokenService>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapIdentityApiV1();

app.UseDefaultOpenApi();

app.Run();