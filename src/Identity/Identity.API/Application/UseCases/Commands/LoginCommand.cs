﻿using System.Threading;
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
    ILogger<LoginHandler> logger
) : IRequestHandler<LoginCommand, AuthTokenDto>
{
    public async Task<AuthTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return new AuthTokenDto("aaa", 123);
    }
}