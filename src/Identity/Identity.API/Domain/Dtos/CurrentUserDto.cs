namespace Identity.API.Domain.Dtos;

public record CurrentUserDto(
    int Id,
    string UserName
    // string Role,
    // string[] Scopes
);