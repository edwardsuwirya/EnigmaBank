using Application.Repositories;
using Common.Wrapper;
using Domain.Contracts;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReadRepositoryAsync<T, TId>(ApplicationDbContext context) : IReadRepositoryAsync<T, TId>
    where T : BaseEntity<TId>
{
    public async Task<T> GetByIdAsync(TId id) => await context.Set<T>().FindAsync(id);

    public async Task<List<T>> GetAllAsync() => await context.Set<T>().ToListAsync();

    public async Task<PageWrapper<List<T>>> GetAllPagingAsync(int page, int pageSize)
    {
        var totalItems = context.Set<T>().Count();
        var result = await context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        return new PageWrapper<List<T>>
        {
            Items = result,
            Page = page,
            PageSize = pageSize,
            CurrentPage = page,
            TotalPages = totalPages,
            TotalItems = totalItems
        };
    }
}