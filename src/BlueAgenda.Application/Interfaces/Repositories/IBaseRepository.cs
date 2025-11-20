using BlueAgenda.Domain.Entities;

namespace BlueAgenda.Application.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> GetById(Guid id);
    TEntity Update(TEntity entity);
    Task<TEntity> ActivateOrDeactivateAsync(TEntity entity, bool inactive = true);
    TEntity Remove(TEntity entity);
    IQueryable<TEntity> Query();
}