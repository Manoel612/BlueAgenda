using BlueAgenda.Application.Models;

namespace BlueAgenda.Application.Interfaces.Repositories;

public interface IContactReadRepository
{
    Task<ContactModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<ContactModel>> GetByUserIdAsync(string aspNetUserId, int page, int pageSize);
}