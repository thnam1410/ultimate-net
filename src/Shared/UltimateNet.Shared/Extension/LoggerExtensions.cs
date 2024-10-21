using Microsoft.AspNetCore.Builder;
using Serilog;
using UltimateNet.Shared.Middlewares;

namespace UltimateNet.Shared.Extension;

public static class LoggerExtensions
{
    public static void AddLogConfigs(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.ReadFrom.Configuration(context.Configuration);
        });
    }
    
    public static void UseLoggerConfigs(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestLogContextMiddleware>();
        
        app.UseSerilogRequestLogging();
    }
}