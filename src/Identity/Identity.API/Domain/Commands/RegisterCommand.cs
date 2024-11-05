using Identity.API.Domain.Dtos;
using MediatR;

namespace Identity.API.Domain.Commands;

public class RegisterCommand: IRequest<bool>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirm { get; set; }
}