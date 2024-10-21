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

    public virtual string UserName => FindClaimOrThrow<string>(JwtRegisteredClaimNames.Name);
    public virtual string Role => FindClaimOrThrow<string>("Role");
    public virtual string[] Scopes => FindClaimOrThrow<string>("Scope").Split(",");
}