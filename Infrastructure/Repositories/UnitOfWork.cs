using System.Collections;
using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class UnitOfWork<TId>(ApplicationDbContext context) : IUnitOfWork<TId>
{
    private bool _disposed;
    private Hashtable _repositories;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }

    public IWriteRepositoryAsync<T, TId> WriteRepositoryFor<T>() where T : BaseEntity<TId>
    {
        _repositories ??= new Hashtable();

        var type = $"{typeof(T).Name}_write";
        if (_repositories.ContainsKey(type)) return (IWriteRepositoryAsync<T, TId>)_repositories[type];
        var repositoryType = typeof(WriteRepositoryAsync<,>);
        var repositoryInstance =
            Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), context);
        _repositories.Add(type, repositoryInstance);
        return (IWriteRepositoryAsync<T, TId>)_repositories[type];
    }

    public IReadRepositoryAsync<T, TId> ReadRepositoryFor<T>() where T : BaseEntity<TId>
    {
        _repositories ??= new Hashtable();

        var type = $"{typeof(T).Name}_read";
        if (_repositories.ContainsKey(type)) return (IReadRepositoryAsync<T, TId>)_repositories[type];
        var repositoryType = typeof(ReadRepositoryAsync<,>);
        var repositoryInstance =
            Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), context);
        _repositories.Add(type, repositoryInstance);
        return (IReadRepositoryAsync<T, TId>)_repositories[type];
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}