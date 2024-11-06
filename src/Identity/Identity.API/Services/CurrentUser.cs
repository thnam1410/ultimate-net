using System;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Identity.API.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public virtual T FindClaimOrThrow<T>(string claimType)
    {
        var claim = User?.FindFirst(claimType);
        if (claim is null)
        {
            throw new InvalidOperationException($"Claim {claimType} not found");
        }

        return (T)Convert.ChangeType(claim.Value, typeof(T));
    }

    public virtual Guid Id => Guid.Parse(FindClaimOrThrow<string>(ClaimTypes.NameIdentifier));
    public virtual string Email => FindClaimOrThrow<string>(ClaimTypes.Email);
    public virtual string FirstName => FindClaimOrThrow<string>(ClaimTypes.GivenName);
    public virtual string LastName => FindClaimOrThrow<string>(ClaimTypes.Surname);
    public virtual string FullName => FindClaimOrThrow<string>(JwtRegisteredClaimNames.Name);

    public virtual string Role => FindClaimOrThrow<string>(ClaimTypes.Role);
    public virtual string[] Scopes => FindClaimOrThrow<string>("Scope").Split(",");


    public virtual ClaimsPrincipal GetClaimsPrincipal() => this.User ?? throw new UnauthorizedAccessException("User not found!");
}