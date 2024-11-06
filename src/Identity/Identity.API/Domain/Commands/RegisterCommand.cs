using Identity.API.Domain.Dtos;
using MediatR;

namespace Identity.API.Domain.Commands;

public class RegisterCommand: IRequest<IResult>
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirm { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}