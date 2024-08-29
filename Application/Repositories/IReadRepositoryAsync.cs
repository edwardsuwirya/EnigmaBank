using Common.Wrapper;
using Domain.Contracts;

namespace Application.Repositories;

public interface IReadRepositoryAsync<T, in TId> where T : class, IEntity<TId>
{
    Task<T> GetByIdAsync(TId id);
    Task<List<T>> GetAllAsync();
    Task<PageWrapper<List<T>>> GetAllPagingAsync(int page, int pageSize);
}