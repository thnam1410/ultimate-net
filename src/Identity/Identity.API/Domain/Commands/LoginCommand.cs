using Identity.API.Domain.Dtos;
using MediatR;

namespace Identity.API.Domain.Commands;

public class LoginCommand: IRequest<AuthTokenDto>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}