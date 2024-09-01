using Application.Extensions;
using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Commands;

public class CreateAccountCommand : IRequest<ResponseWrapper<int>>
{
    public CreateAccount CreateAccount { get; set; }
}

public class CreateAccountCommandHandler(IUnitOfWork<int> unitOfWork, IValidator<CreateAccount> validator)
    : IRequestHandler<CreateAccountCommand, ResponseWrapper<int>>
{
    public async Task<ResponseWrapper<int>> Handle(CreateAccountCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request.CreateAccount, cancellationToken);
        var res = validationResult.ValidationResponse<int>();
        if (res is not null) return res;

        var account = request.CreateAccount.Adapt<Account>();
        account.AccountNumber = AccountNumberGenerator.Generate();
        account.IsActive = true;

        await unitOfWork.WriteRepositoryFor<Account>().AddAsync(account);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ResponseWrapper<int>().Success(account.Id, "Account created");
    }
}