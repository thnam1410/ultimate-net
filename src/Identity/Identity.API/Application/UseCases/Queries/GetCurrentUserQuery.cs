using System.Linq;
using System.Security.Claims;
using System.Threading;
using Identity.API.Domain.Dtos;
using Identity.API.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UltimateNet.Shared.Endpoint;

namespace Identity.API.UseCases.Queries;

public class GetCurrentUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("me", async (ISender sender) => await sender.Send(new GetCurrentUserQuery())).RequireAuthorization();
    }
}

public record GetCurrentUserQuery : IRequest<CurrentUserDto>
{
}

internal class GetCurrentUserQueryHandler(
    ICurrentUser currentUser,
    ILogger<GetCurrentUserQueryHandler> logger,
    IConfiguration config
) : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
{
    public async Task<CurrentUserDto> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var claims = currentUser.GetClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value);

        return new CurrentUserDto(
            1,
            currentUser.UserName
            // currentUser.Role,
            // currentUser.Scopes
        );
    }
}