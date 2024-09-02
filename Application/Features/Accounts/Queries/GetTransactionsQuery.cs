using Application.Repositories;
using Common.Exceptions;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetTransactionsQuery() : IRequest<ResponseWrapper<List<TransactionResponse>>>
{
    public int AccountId { get; set; }
}

public class GetAccountTransactionQueryHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<GetTransactionsQuery,
        ResponseWrapper<List<TransactionResponse>>>
{
    public async Task<ResponseWrapper<List<TransactionResponse>>> Handle(GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactionsInDb = await unitOfWork
            .ReadRepositoryFor<Transaction>()
            .FilterByAsync(trx => trx.AccountId == request.AccountId);

        return transactionsInDb.Count == 0
            ? ResponseWrapper<List<TransactionResponse>>.Fail(ExistenceErrors.EmptyList)
            : ResponseWrapper<List<TransactionResponse>>.Success(transactionsInDb.Adapt<List<TransactionResponse>>());
    }
}