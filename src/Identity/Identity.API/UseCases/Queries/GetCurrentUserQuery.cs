using System.Security.Claims;
using System.Threading;
using Identity.API.Domain.Dtos;
using Identity.API.Services;
using MediatR;
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

internal class GetCurrentUserQueryHandler(ICurrentUser currentUser) : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
{
    public async Task<CurrentUserDto> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken
    )
    {
        return new CurrentUserDto(
            1,
            currentUser.UserName,
            currentUser.Role,
            currentUser.Scopes
        );
    }
}