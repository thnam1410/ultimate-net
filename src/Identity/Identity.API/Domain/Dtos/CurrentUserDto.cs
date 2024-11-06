using System;

namespace Identity.API.Domain.Dtos;

public record CurrentUserDto(
    Guid Id,
    string Email,
    // string UserName,
    string FirstName,
    string LastName,
    string FullName
    // string Role
    // string[] Scopes
);