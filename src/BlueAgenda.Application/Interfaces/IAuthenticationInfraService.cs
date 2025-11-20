using BlueAgenda.Application.Models;
using BlueAgenda.Domain.Entities;

namespace BlueAgenda.Application.Interfaces;

public interface IAuthenticationInfraService
{
    Task<User> RegisterAsync(CreateUserModel model);
    Task<string> LoginAsync(string email, string password);
}