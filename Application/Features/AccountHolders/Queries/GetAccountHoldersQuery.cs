using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Queries;

public class GetAccountHoldersQuery() : IRequest<ResponseWrapper<List<AccountHolderResponse>>>
{
    public int Id { get; set; }
}

public class GetAccountHoldersQueryHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<GetAccountHoldersQuery,
    ResponseWrapper<List<AccountHolderResponse>>>
{
    public async Task<ResponseWrapper<List<AccountHolderResponse>>> Handle(GetAccountHoldersQuery request,
        CancellationToken cancellationToken)
    {
        var accountHoldersInDb = await unitOfWork.ReadRepositoryFor<AccountHolder>().GetAllAsync();

        if (accountHoldersInDb.Count == 0)
            return new ResponseWrapper<List<AccountHolderResponse>>().Fail("No account holders found");
        return new ResponseWrapper<List<AccountHolderResponse>>()
            .Success(accountHoldersInDb.Adapt<List<AccountHolderResponse>>());
    }
}