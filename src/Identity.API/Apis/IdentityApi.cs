﻿using System.Threading.Tasks;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Identity.API.Apis;

public static class IdentityApi
{
    public static IEndpointRouteBuilder MapIdentityApiV1(this IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet("Identity")
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("api/v{version:apiVersion}/identity")
            .WithApiVersionSet(apiVersionSet);

        group.MapGet("/me", GetCurrentUserInfo);

        return app;
    }

    private static async Task<Results<Ok<string>, BadRequest<string>>> GetCurrentUserInfo()
    {
        await Task.CompletedTask;
        return TypedResults.Ok("Hello from Identity Service!");
    }
}