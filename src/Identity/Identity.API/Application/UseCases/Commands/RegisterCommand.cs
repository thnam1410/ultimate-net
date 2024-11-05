using System.Linq;
using System.Threading;
using FluentValidation;
using Identity.API.Domain.Commands;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Users;
using Keycloak.AuthServices.Sdk.Protection;
using MediatR;
using Microsoft.Extensions.Options;
using UltimateNet.Shared.Endpoint;

namespace Identity.API.UseCases.Commands;

public class RegisterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("register",
            async (RegisterCommand command, ISender sender) => await sender.Send(command));
    }
}

internal class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("User name can't be empty.");
        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password can't be empty.");
        RuleFor(command => command.PasswordConfirm)
            .Equal(command => command.Password)
            .WithMessage("Passwords do not match.");
    }
}

internal class RegisterHandler(
    IOptionsSnapshot<KeycloakAdminClientOptions> options,
    IKeycloakUserClient keycloakUserClient,
    IKeycloakRealmClient keycloakRealmClient,
    IKeycloakClient keycloakClient
) : IRequestHandler<RegisterCommand, bool>
{
    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isExist = await keycloakUserClient.GetUserCountAsync(options.Value.Realm, new GetUserCountRequestParameters
        {
            Email = request.Email,
        }, cancellationToken);
        
        if (isExist > 0) throw new BadHttpRequestException("User already exists.");
        
        return true;
    }
}