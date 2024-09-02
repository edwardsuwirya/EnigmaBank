using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Commands;

public class CreateAccountCommand : IRequest<ResponseWrapper<int>>
{
    public CreateAccount CreateAccount { get; set; }
}

public class CreateAccountCommandHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<CreateAccountCommand, ResponseWrapper<int>>
{
    public async Task<ResponseWrapper<int>> Handle(CreateAccountCommand request,
        CancellationToken cancellationToken)
    {
        var account = request.CreateAccount.Adapt<Account>();
        account.AccountNumber = AccountNumberGenerator.Generate();
        account.IsActive = true;

        await unitOfWork.WriteRepositoryFor<Account>().AddAsync(account);
        await unitOfWork.CommitAsync(cancellationToken);
        return ResponseWrapper<int>.Success(account.Id, "Account created");
    }
}