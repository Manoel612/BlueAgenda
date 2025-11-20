using BlueAgenda.Application.Interfaces;
using BlueAgenda.Application.Models;
using BlueAgenda.Domain.Entities;

namespace BlueAgenda.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationInfraService AuthenticationInfraService;

    public AuthenticationService(
        IAuthenticationInfraService authenticationInfraService)
    {
        AuthenticationInfraService = authenticationInfraService;
    }

    public async Task<User> RegisterAsync(CreateUserModel model)
    {
        return await AuthenticationInfraService.RegisterAsync(model);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        return await AuthenticationInfraService.LoginAsync(email, password);
    }
}
