using Application.Repositories;
using Common.Exceptions;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountTransactionsQuery() : IRequest<ResponseWrapper<AccountTransactionsResponse>>
{
    public int Id { get; set; }
}

public class GetAccountTransactionsQueryHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<GetAccountTransactionsQuery,
        ResponseWrapper<AccountTransactionsResponse>>
{
    public async Task<ResponseWrapper<AccountTransactionsResponse>> Handle(GetAccountTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactionsInDb = await unitOfWork
            .ReadRepositoryFor<Account>()
            .FilterByAsync(acc => acc.Id == request.Id, trx => trx.Transactions);

        return transactionsInDb.Count == 0
            ? ResponseWrapper<AccountTransactionsResponse>.Fail(ExistenceErrors.EmptyList)
            : ResponseWrapper<AccountTransactionsResponse>.Success(transactionsInDb.FirstOrDefault()
                .Adapt<AccountTransactionsResponse>());
    }
}