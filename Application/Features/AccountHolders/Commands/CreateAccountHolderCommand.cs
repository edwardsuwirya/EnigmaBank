using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Commands;

public class CreateAccountHolderCommand : IRequest<ResponseWrapper<int>>
{
    public CreateAccountHolder CreateAccountHolder { get; set; }
}

public class CreateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<CreateAccountHolderCommand, ResponseWrapper<int>>
{
    public async Task<ResponseWrapper<int>> Handle(CreateAccountHolderCommand request,
        CancellationToken cancellationToken)
    {
        var accountHolder = request.CreateAccountHolder.Adapt<AccountHolder>();
        await unitOfWork.WriteRepositoryFor<AccountHolder>().AddAsync(accountHolder);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ResponseWrapper<int>().Success(accountHolder.Id, "Account Holder created");
    }
}