using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Queries;

public class GetAccountHoldersPagingQuery() : IRequest<ResponseWrapper<List<AccountHolderResponse>>>
{
    public int page { get; set; }
    public int size { get; set; }
}

public class GetAccountHoldersQueryPagingHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<GetAccountHoldersPagingQuery,
        ResponseWrapper<List<AccountHolderResponse>>>
{
    public async Task<ResponseWrapper<List<AccountHolderResponse>>> Handle(GetAccountHoldersPagingQuery request,
        CancellationToken cancellationToken)
    {
        var accountHoldersInDb = await unitOfWork.ReadRepositoryFor<AccountHolder>()
            .GetAllPagingAsync(request.page, request.size);

        if (accountHoldersInDb.Items.Count == 0)
            return new ResponseWrapper<List<AccountHolderResponse>>().Fail("No account holders found");
        
        return new PagingResponseWrapper<List<AccountHolderResponse>>().Success(
            accountHoldersInDb.Adapt<PageWrapper<List<AccountHolderResponse>>>());
    }
}