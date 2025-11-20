using BlueAgenda.Application.Models;

namespace BlueAgenda.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<UserModel> RegisterAsync(CreateUserModel model);
    Task<string> LoginAsync(string email, string password);
}