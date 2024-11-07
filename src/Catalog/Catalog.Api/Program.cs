using Asp.Versioning;
using Asp.Versioning.Builder;
using Catalog.Api.Extensions;
using FluentValidation;
using UltimateNet.ServiceDefaults;
using UltimateNet.Shared.Cqrs.Behaviours;
using UltimateNet.Shared.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.AddLogConfigs();
builder.AddApiVersioning();
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddProblemDetails();

builder
    .AddApplicationServices()
    .AddApiServices()
    ;

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGraphQL();

app.UseLoggerConfigs();

app.Run();