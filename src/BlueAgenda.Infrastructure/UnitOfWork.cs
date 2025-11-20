using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Infrastructure.Data;

namespace BlueAgenda.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlueAgendaDbContext Context;

    public UnitOfWork(BlueAgendaDbContext context)
    {
        Context = context;
    }

    public Task<int> SaveChangesAsync()
        => Context.SaveChangesAsync();
}