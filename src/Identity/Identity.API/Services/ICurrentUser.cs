using System;
using System.Security.Claims;

namespace Identity.API.Services;

public interface ICurrentUser
{
    Guid Id { get; }
    // string UserName { get; }
    string Email { get; }
    
    string FirstName { get; }
    string LastName { get; }
    string FullName { get; }
    string Role { get; }
    string[] Scopes { get; }

    ClaimsPrincipal GetClaimsPrincipal();
}