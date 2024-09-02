using Application.Repositories;
using Common.Exceptions;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountByAccountNumberQuery() : IRequest<ResponseWrapper<AccountResponse>>
{
    public string AccountNumber { get; set; }
}

public class GetAccountByAccountNumberQueryHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<GetAccountByAccountNumberQuery,
        ResponseWrapper<AccountResponse>>
{
    public async Task<ResponseWrapper<AccountResponse>> Handle(GetAccountByAccountNumberQuery request,
        CancellationToken cancellationToken)
    {
        var accountInDb =
            unitOfWork.ReadRepositoryFor<Account>()
                .Entities
                .FirstOrDefault(account => account.AccountNumber == request.AccountNumber);

        if (accountInDb is null)
            return await Task.FromResult(
                ResponseWrapper<AccountResponse>.Fail(ExistenceErrors.NotFound(request.AccountNumber)));

        return await Task.FromResult(ResponseWrapper<AccountResponse>.Success(
            accountInDb.Adapt<AccountResponse>()));
    }
}