using Application.Repositories;
using Common.Exceptions;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Queries;

public class GetAccountHolderByIdQuery() : IRequest<ResponseWrapper<AccountHolderResponse>>
{
    public int Id { get; set; }
}

public class GetAccountHolderByIdQueryHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<GetAccountHolderByIdQuery,
    ResponseWrapper<AccountHolderResponse>>
{
    public async Task<ResponseWrapper<AccountHolderResponse>> Handle(GetAccountHolderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var accountHolderInDb =
            await unitOfWork.ReadRepositoryFor<AccountHolder>().GetByIdAsync(request.Id);
        if (accountHolderInDb is null)
            return new ResponseWrapper<AccountHolderResponse>(ExistenceErrors.NotFound(request.Id.ToString()));
        return new ResponseWrapper<AccountHolderResponse>(
            accountHolderInDb.Adapt<AccountHolderResponse>());
    }
}