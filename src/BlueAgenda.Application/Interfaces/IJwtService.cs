using BlueAgenda.Domain.Entities;

namespace BlueAgenda.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}