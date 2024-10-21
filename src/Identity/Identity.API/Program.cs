using Asp.Versioning;
using Asp.Versioning.Builder;
using FluentValidation;
using Identity.API.Services;
using Microsoft.Extensions.Hosting;
using UltimateNet.Shared.Cqrs.Behaviours;
using UltimateNet.Shared.Endpoint;
using UltimateNet.Shared.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.AddLogConfigs();

builder.AddDefaultOpenApi();
builder.Services.AddEndpoints(typeof(Program).Assembly);

builder.AddDefaultAuthentication();

builder.Services.AddProblemDetails();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    
    cfg.AddOpenBehavior(typeof(RequestLoggingBehaviour<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>(includeInternalTypes: true);

builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddTransient<ICurrentUser, CurrentUser>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseLoggerConfigs();

ApiVersionSet apiVersionSet = app.NewApiVersionSet("Identity")
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

var identityVersionedGroup = app.MapGroup("api/v{version:apiVersion}/identity")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(identityVersionedGroup);

app.Run();