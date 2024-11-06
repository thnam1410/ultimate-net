using System.Threading;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;

namespace Identity.API.Application.Services;

public class UserService(
    IKeycloakUserClient keycloakUserClient
): IUserService
{
    private readonly IKeycloakUserClient _keycloakUserClient = keycloakUserClient;

    public Task GetUserById(string id, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}