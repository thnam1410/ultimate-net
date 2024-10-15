namespace ApiGateway.Extensions;

public static class OcelotExtensions
{
    public static IHostApplicationBuilder AddRouteConfigs(this IHostApplicationBuilder builder)
    {
        var ocelotJsonFiles = Directory.GetFiles("./Routes", "*.ocelot.json");
        
        foreach (var jsonFile in ocelotJsonFiles)
        {
            builder.Configuration.AddJsonFile(jsonFile, optional: false, reloadOnChange: true);
        }

        builder.Services.AddOcelot(builder.Configuration);
        return builder;
    }
}