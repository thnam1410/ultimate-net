﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Identity.API.Domain.Commands;
using Identity.API.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UltimateNet.Shared.Extension;

namespace Identity.API.Services;

public record User(string Username, string Password, string Role, string[] Scopes);

public class JwtTokenService
{
    private readonly IConfiguration _config;

    private readonly List<User> _user = new()
    {
        new("admin", "admin", "Administrator", ["writers.read"]),
        new("user", "user", "User", ["writers.noread"]),
    };

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<AuthTokenDto> GenerateAuthToken(LoginCommand request)
    {
        var user = await Task.FromResult(
            _user.FirstOrDefault(u => u.Username == request.UserName && u.Password == request.Password)
        );

        if (user is null)
            throw new UnauthorizedAccessException("User not found!");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetRequiredValue("Jwt:SecretKey")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expireConfig = _config.GetSection("Jwt:Expiration").Get<int?>();

        var claims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Name, user.Username),
            new ("Role", user.Role),
            new ("Scope", string.Join(",", user.Scopes)),
        };

        var expirationTimestamp = DateTime.Now.AddMinutes(expireConfig ?? 60);
        var securityToken = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: expirationTimestamp,
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new AuthTokenDto(token, (int)expirationTimestamp.Subtract(DateTime.Now).TotalSeconds);
    }
}