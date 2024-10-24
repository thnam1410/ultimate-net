﻿using System.Security.Claims;

namespace Identity.API.Services;

public interface ICurrentUser
{
    string UserName { get; }
    string Role { get; }
    string[] Scopes { get; }

    ClaimsPrincipal GetClaimsPrincipal();
}