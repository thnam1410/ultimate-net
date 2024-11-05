using System.Net.Http.Headers;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Sdk;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using UltimateNet.Shared.Extension;

namespace Identity.API.Infrastructure;

public static class AuthExtensions
{
    public static IHostApplicationBuilder AddAuth(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddKeycloakWebApiAuthentication(builder.Configuration);
        builder.Services.AddAuthorization();

        var keycloakConfig = builder.Configuration.GetSection(
            KeycloakAuthenticationOptions.Section
        ).Get<KeycloakAdminClientOptions>()!;

        var configuration = new KeycloakAdminClientOptions()
        {
            AuthServerUrl = keycloakConfig.AuthServerUrl,
            Realm = keycloakConfig.Realm,
            Resource = keycloakConfig.Resource,
            VerifyTokenAudience = false,
            SslRequired = "none",
            Credentials = { Secret = keycloakConfig.Credentials.Secret }
        };

        var tokenClientName = "admin-api";
        builder.Services.AddDistributedMemoryCache();
        builder.Services
            .AddClientCredentialsTokenManagement()
            .AddClient(
                tokenClientName,
                client =>
                {
                    client.ClientId = keycloakConfig.Resource;
                    client.ClientSecret = keycloakConfig.Credentials.Secret;
                    client.TokenEndpoint = keycloakConfig.KeycloakTokenEndpoint;
                }
            );

        builder.Services
            .AddKeycloakAdminHttpClient(configuration)
            .AddClientCredentialsTokenHandler(tokenClientName)
            .AddStandardResilienceHandler()
            ;

        return builder;
    }
}