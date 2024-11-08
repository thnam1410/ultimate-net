using Catalog.Api.Apis;
using Catalog.Api.Infrastructure;
using FluentValidation;
using UltimateNet.Shared.Cqrs.Behaviours;

namespace Catalog.Api.Extensions;

public static class Extensions
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddGraphQLServer()
            .AddQueryType<CatalogQuery>()
            .AddMutationType<CatalogMutation>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            ;

        return builder;
    }
    
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddOrm();
        builder.AddMediatR();
        
        return builder;
    }

    private static void AddOrm(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CatalogDbContext>("CatalogDb", configureDbContextOptions: dbContextOptionsBuilder =>
        {
            dbContextOptionsBuilder.UseNpgsql(builder =>
            {
                //TODO: Research & Apply vector extension
                // builder.UseVector();
            });
        });

        // Dev/Sample purpose only
        builder.Services.AddMigration<CatalogDbContext, CatalogDbContextSeed>();
    }

    private static void AddMediatR(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();

            cfg.AddOpenBehavior(typeof(RequestLoggingBehaviour<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssemblyContaining<Program>(includeInternalTypes: true);
    }
}