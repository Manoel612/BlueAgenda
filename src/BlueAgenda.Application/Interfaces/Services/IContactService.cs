using BlueAgenda.Application.Models;

namespace BlueAgenda.Application.Interfaces.Services;

public interface IContactService
{
    Task<ContactModel> GetByIdAsync(Guid id);
    Task<IEnumerable<ContactModel>> GetByUserIdAsync(string userId, int page, int pageSize);
    Task<ContactModel> RegisterAsync(string userId, CreateContactModel model);
    Task<ContactModel> UpdateAsync(Guid id, UpdateContactModel model);
    Task<ContactModel> DeactivateAsync(Guid id);
}
