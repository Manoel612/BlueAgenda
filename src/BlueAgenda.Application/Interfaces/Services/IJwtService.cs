using BlueAgenda.Application.Models;

namespace BlueAgenda.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(UserModel user);
}