using Microsoft.AspNetCore.Routing;

namespace UltimateNet.Shared.Endpoint;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}