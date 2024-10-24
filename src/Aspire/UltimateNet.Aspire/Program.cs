var builder = DistributedApplication.CreateBuilder(args);


var keycloak = builder
    .AddKeycloak("ultimate-net", 18080)
    .WithDataVolume()
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
    .WithImageTag("latest");

builder.AddProject<Projects.Identity_API>("identity-api")
    .WithReference(keycloak);

builder.Build().Run();