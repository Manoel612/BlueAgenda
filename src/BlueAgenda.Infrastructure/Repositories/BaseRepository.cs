using BlueAgenda.Application.Interfaces.Repositories;
using BlueAgenda.Domain.Entities;
using BlueAgenda.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlueAgenda.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    BlueAgendaDbContext DbContext { get; }
    DbSet<TEntity> DbSet { get; }

    public BaseRepository(BlueAgendaDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var newEntity = await DbSet.AddAsync(entity);
        return newEntity.Entity;
    }

    public async Task<TEntity> GetById(Guid id)
    {
        var entity = await DbSet.FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new KeyNotFoundException($"{typeof(TEntity).Name} with id {id} not found.");

        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        var newEntity = DbSet.Update(entity);
        return newEntity.Entity;
    }

    public TEntity ActivateOrDeactivate(TEntity entity, bool inactive = true)
    {
        entity.InactivatedAt = inactive ? DateTime.UtcNow : null;

        return Update(entity);
    }

    public TEntity Remove(TEntity entity)
    {
        var newEntity = DbSet.Remove(entity);
        return newEntity.Entity;
    }

    public IQueryable<TEntity> Query()
    {
        return DbSet.AsQueryable();
    }

}