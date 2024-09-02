using Application.Repositories;
using Common.Exceptions;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountsQuery() : IRequest<ResponseWrapper<List<AccountResponse>>>;

public class GetAccountsQueryHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<GetAccountsQuery,
    ResponseWrapper<List<AccountResponse>>>
{
    public async Task<ResponseWrapper<List<AccountResponse>>> Handle(GetAccountsQuery request,
        CancellationToken cancellationToken)
    {
        var accountsInDb = await unitOfWork.ReadRepositoryFor<Account>().GetAllAsync();

        return accountsInDb.Count == 0
            ? ResponseWrapper<List<AccountResponse>>.Fail(ExistenceErrors.EmptyList)
            : ResponseWrapper<List<AccountResponse>>.Success(accountsInDb.Adapt<List<AccountResponse>>());
    }
}