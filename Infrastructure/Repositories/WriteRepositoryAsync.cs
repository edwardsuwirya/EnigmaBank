using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class WriteRepositoryAsync<T, TId>(ApplicationDbContext context)
    : IWriteRepositoryAsync<T, TId> where T : BaseEntity<TId>
{
    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var currentEntity = await context.Set<T>().FindAsync(entity.Id);
        context.Entry(currentEntity).CurrentValues.SetValues(entity);
        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}