using System;
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
        app.MapGet("test", () => "Hello World!");
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
        return new CurrentUserDto(
            currentUser.Id,
            currentUser.Email,
            currentUser.FirstName,
            currentUser.LastName,
            currentUser.FullName
            // currentUser.Role
            // currentUser.Scopes
        );
    }
}