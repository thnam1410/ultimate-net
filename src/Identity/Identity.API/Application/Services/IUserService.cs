using System.Threading;
using Identity.API.Domain.Commands;
using Keycloak.AuthServices.Sdk.Admin.Models;

namespace Identity.API.Application.Services;

public interface IUserService
{ 
    Task GetUserById(string id, CancellationToken cancellationToken);
}