using BlueAgenda.Application.Models;

namespace BlueAgenda.Application.Interfaces.Services;

public interface IContactService
{
    Task<ContactModel> GetByIdAsync(Guid id);
    Task<UserContactsModel> GetByUserIdAsync(string userId, int page, int pageSize);
    Task<ContactModel> RegisterAsync(string userId, CreateContactModel model);
    Task<ContactModel> UpdateAsync(string userId, Guid id, UpdateContactModel model);
    Task<ContactModel> DeactivateAsync(string userId, Guid id);
}
