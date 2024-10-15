using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Product.API.Apis;

public static class ProductApi
{
    public static IEndpointRouteBuilder MapProductApiV1(this IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet("Product")
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("api/v{version:apiVersion}/product")
            .WithApiVersionSet(apiVersionSet);

        group.MapGet("/", GetListProducts);

        return app;
    }

    private static async Task<Results<Ok<string>, BadRequest<string>>> GetListProducts()
    {
        await Task.CompletedTask;
        return TypedResults.Ok("List products!");
    }
}