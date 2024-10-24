using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace UltimateNet.Shared.Extension;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenWithAuth(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

            o.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(config.GetRequiredValue("Keycloak:AuthUrl")),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "openid" },
                            { "profile", "profile" },
                            { "audience-scope", "audience-scope" },
                        }
                    }
                }
            });

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Keycloak",
                            Type = ReferenceType.SecurityScheme,
                        },
                        In = ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = "Bearer",
                    },
                    []
                },
            };

            o.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}