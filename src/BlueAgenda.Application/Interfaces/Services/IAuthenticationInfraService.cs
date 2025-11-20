using BlueAgenda.Application.Models;

namespace BlueAgenda.Application.Interfaces.Services;

public interface IAuthenticationInfraService
{
    Task<UserModel> RegisterAsync(CreateUserModel model);
    Task<string> LoginAsync(string email, string password);
}