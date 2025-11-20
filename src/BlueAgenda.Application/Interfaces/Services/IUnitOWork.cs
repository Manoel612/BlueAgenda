namespace BlueAgenda.Application.Interfaces.Services;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}