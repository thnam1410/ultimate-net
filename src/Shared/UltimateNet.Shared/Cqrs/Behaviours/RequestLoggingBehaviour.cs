using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace UltimateNet.Shared.Cqrs.Behaviours;

public class RequestLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : notnull
{
    private readonly ILogger<RequestLoggingBehaviour<TRequest, TResponse>> _logger;

    public RequestLoggingBehaviour(ILogger<RequestLoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Processing request {RequestName}", requestName);

        try
        {
            TResponse result = await next();

            _logger.LogInformation("Completed request {RequestName}", requestName);

            return result;
        }
        catch (Exception err)
        {
            using (LogContext.PushProperty("Error", err, true))
            {
                _logger.LogError("Completed request {RequestName} with error", requestName);
            }

            throw;
        }
    }
}