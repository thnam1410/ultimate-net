using System.Threading;
using FluentValidation;
using Identity.API.Domain.Commands;
using Identity.API.Domain.Dtos;
using Identity.API.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UltimateNet.Shared.Endpoint;

namespace Identity.API.UseCases.Commands;

public class LoginEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login", async (LoginCommand command, ISender sender) => await sender.Send(command));
    }
}

internal class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(command => command.UserName)
            .NotEmpty().WithMessage("User name can't be empty.");
        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password can't be empty.");
    }
}

internal class LoginHandler(
    JwtTokenService jwtTokenService,
    ILogger<LoginHandler> logger,
    IConfiguration configuration
) : IRequestHandler<LoginCommand, AuthTokenDto>
{
    public async Task<AuthTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling login request for user {UserName}", request.UserName);
        logger.LogInformation(configuration["Authentication:Audience"]);
        logger.LogInformation(configuration["Authentication:MetadataAddress"]);
        logger.LogInformation(configuration["Authentication:ValidIssuer"]);

        return new AuthTokenDto("aaa", 123);
    }
}