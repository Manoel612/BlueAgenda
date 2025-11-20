using BlueAgenda.Application.Interfaces.Repositories;
using BlueAgenda.Domain.Entities;
using BlueAgenda.Infrastructure.Data;

namespace BlueAgenda.Infrastructure.Repositories;

public class ContactRepository : BaseRepository<Contact>, IContactRepository
{
    public ContactRepository(BlueAgendaDbContext dbContext) : base(dbContext)
    {
    }
}