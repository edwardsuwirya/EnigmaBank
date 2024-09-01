using Application.Repositories;
using Common.Exceptions;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountByIdQuery() : IRequest<ResponseWrapper<AccountResponse>>
{
    public int Id { get; set; }
}

public class GetAccountByIdQueryHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<GetAccountByIdQuery,
    ResponseWrapper<AccountResponse>>
{
    public async Task<ResponseWrapper<AccountResponse>> Handle(GetAccountByIdQuery request,
        CancellationToken cancellationToken)
    {
        var accountInDb =
            await unitOfWork.ReadRepositoryFor<Account>().GetByIdAsync(request.Id, c => c.AccountHolder);
        if (accountInDb is null)
            return new ResponseWrapper<AccountResponse>().Fail(ExistenceErrors.NotFound(request.Id.ToString()));
        return new ResponseWrapper<AccountResponse>().Success(
            accountInDb.Adapt<AccountResponse>());
    }
}