using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReadRepositoryAsync<T, TId>(ApplicationDbContext context) : IReadRepositoryAsync<T, TId>
    where T : BaseEntity<TId>
{
    public async Task<T> GetByIdAsync(TId id) => await context.Set<T>().FindAsync(id);

    public async Task<List<T>> GetAllAsync() => await context.Set<T>().ToListAsync();
}