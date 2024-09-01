using Application.Extensions;
using Application.Repositories;
using Common.Enums;
using Common.Exceptions;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using Domain;
using FluentValidation;
using Mapster;
using MediatR;
using Transaction = Domain.Transaction;

namespace Application.Features.Accounts.Commands;

public class CreateTransactionCommand : IRequest<ResponseWrapper<int>>
{
    public TransactionRequest Transaction { get; set; }
}

public class CreateTransactionCommandHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<CreateTransactionCommand, ResponseWrapper<int>>
{
    public async Task<ResponseWrapper<int>> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var accountInDb = await unitOfWork.ReadRepositoryFor<Account>().GetByIdAsync(request.Transaction.AccountId);
        if (accountInDb == null)
            return new ResponseWrapper<int>(ExistenceErrors.NotFound(request.Transaction.AccountId.ToString()));

        switch (request.Transaction.Type)
        {
            case TransactionType.Deposit:
                var transactionDeposit = new Transaction
                {
                    AccountId = accountInDb.Id,
                    Amount = request.Transaction.Amount,
                    Type = TransactionType.Deposit,
                    Date = DateTime.Now,
                };
                accountInDb.Balance += request.Transaction.Amount;
                await unitOfWork.WriteRepositoryFor<Transaction>().AddAsync(transactionDeposit);
                await unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
                await unitOfWork.CommitAsync(cancellationToken);
                return new ResponseWrapper<int>(data: transactionDeposit.Id,
                    "Deposit is successfully created.");
            case TransactionType.Withdrawal:
                if (request.Transaction.Amount > accountInDb.Balance)
                    return new ResponseWrapper<int>(BusinessErrors.InsufficientBalance);

                var transactionWithdrawal = new Transaction
                {
                    AccountId = accountInDb.Id,
                    Amount = request.Transaction.Amount,
                    Type = TransactionType.Withdrawal,
                    Date = DateTime.Now,
                };
                accountInDb.Balance -= request.Transaction.Amount;
                await unitOfWork.WriteRepositoryFor<Transaction>().AddAsync(transactionWithdrawal);
                await unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
                await unitOfWork.CommitAsync(cancellationToken);
                return new ResponseWrapper<int>(data: transactionWithdrawal.Id,
                    "Withdrawal is successfully created.");
            default:
                return new ResponseWrapper<int>(GeneralErrors.General("Transaction Type is invalid"));
        }
    }
}