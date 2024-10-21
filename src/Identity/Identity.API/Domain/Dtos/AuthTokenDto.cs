namespace Identity.API.Domain.Dtos;

public record AuthTokenDto(string Token, int ExpiresIn);
