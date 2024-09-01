using System.Linq.Expressions;
using Common.Wrapper;
using Domain.Contracts;

namespace Application.Repositories;

public interface IReadRepositoryAsync<T, in TId> where T : class, IEntity<TId>
{
    Task<T> GetByIdAsync(TId id, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<List<T>> FilterByAsync(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includes);
    Task<PageWrapper<List<T>>> GetAllPagingAsync(int page, int pageSize, params Expression<Func<T, object>>[] includes);

    IQueryable<T> Entities { get; }
}