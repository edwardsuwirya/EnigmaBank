using System.Linq.Expressions;
using Application.Repositories;
using Common.Wrapper;
using Domain.Contracts;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReadRepositoryAsync<T, TId>(ApplicationDbContext context) : IReadRepositoryAsync<T, TId>
    where T : BaseEntity<TId>
{
    public async Task<T> GetByIdAsync(TId id, params Expression<Func<T, object>>[] includes) =>
        await includes
            .Aggregate(context.Set<T>().AsQueryable(), (curr, inc) => curr.Include(inc)).Where(e => e.Id.Equals(id))
            .FirstOrDefaultAsync();


    public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes) => await includes
        .Aggregate(context.Set<T>().AsQueryable(), (curr, inc) => curr.Include(inc)).ToListAsync();

    public async Task<List<T>> FilterByAsync(Expression<Func<T, bool>> whereClause,
        params Expression<Func<T, object>>[] includes) =>
        await includes
            .Aggregate(context.Set<T>().Where(whereClause).AsQueryable(), (curr, inc) => curr.Include(inc))
            .ToListAsync();


    public async Task<PageWrapper<List<T>>> GetAllPagingAsync(int page, int pageSize,
        params Expression<Func<T, object>>[] includes)
    {
        var totalItems = context.Set<T>().Count();
        var result = await
            includes.Aggregate(context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).AsQueryable(),
                    (curr, inc) => curr.Include(inc))
                .ToListAsync();
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

    public IQueryable<T> Entities => context.Set<T>();
}