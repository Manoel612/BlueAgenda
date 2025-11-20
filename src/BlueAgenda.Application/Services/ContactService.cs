using AutoMapper;
using BlueAgenda.Application.Interfaces.Repositories;
using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Application.Models;
using BlueAgenda.Domain.Entities;

namespace BlueAgenda.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository Repository;
    private readonly IContactReadRepository ReadRepository;
    private readonly IMapper Mapper;
    private readonly IUnitOfWork UnitOfWork;

    public ContactService(IContactRepository repository, IContactReadRepository readRepository, IUnitOfWork unitOfWorkService, IMapper mapper)
    {
        ReadRepository = readRepository;
        Mapper = mapper;
        Repository = repository;
        UnitOfWork = unitOfWorkService;
    }

    public async Task<ContactModel> GetByIdAsync(Guid id)
    {
        var contact = await ReadRepository.GetByIdAsync(id) ??
            throw new KeyNotFoundException($"Contact with id {id} not found");

        return contact;
    }

    public async Task<IEnumerable<ContactModel>> GetByUserIdAsync(
        string userId,
        int page = 1,
        int pageSize = 20)
    {
        var contacts = await ReadRepository.GetByUserIdAsync(
            aspNetUserId: userId,
            page: page,
            pageSize: pageSize
        );

        return contacts;
    }

    public async Task<ContactModel> RegisterAsync(string userId, CreateContactModel model)
    {
        var entity = Mapper.Map<Contact>(model);
        entity.AspNetUserId = userId;

        var newContact = await Repository.AddAsync(entity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<ContactModel>(newContact);
    }

    public async Task<ContactModel> UpdateAsync(Guid id, UpdateContactModel model)
    {
        var entity = await Repository.GetById(id);

        Mapper.Map(model, entity);

        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<ContactModel>(entity);
    }

    public async Task<ContactModel> DeactivateAsync(Guid id)
    {
        var newContact = await Repository.ActivateOrDeactivateAsync(id);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<ContactModel>(newContact);
    }

}
