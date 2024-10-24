using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace UltimateNet.Shared.Extension;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.Audience = builder.Configuration.GetRequiredValue("Authentication:Audience");
                o.MetadataAddress = builder.Configuration.GetRequiredValue("Authentication:MetadataAddress");
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = builder.Configuration.GetRequiredValue("Authentication:ValidIssuer"),
                };
            });

        services.AddAuthorization();

        return services;
    }

    public static WebApplication UseDefaultAuth(this WebApplication app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}