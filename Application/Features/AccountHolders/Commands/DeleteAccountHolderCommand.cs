using Application.Repositories;
using Common.Exceptions;
using Common.Wrapper;
using Domain;
using MediatR;

namespace Application.Features.AccountHolders.Commands;

public class DeleteAccountHolderCommand : IRequest<ResponseWrapper<int>>
{
    public int Id { get; set; }
}

public class DeleteAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<DeleteAccountHolderCommand, ResponseWrapper<int>>
{
    public async Task<ResponseWrapper<int>> Handle(DeleteAccountHolderCommand request,
        CancellationToken cancellationToken)
    {
        var accountHolderInDb =
            await unitOfWork.ReadRepositoryFor<AccountHolder>().GetByIdAsync(request.Id);
        if (accountHolderInDb is null)
            return ResponseWrapper<int>.Fail(
                ExistenceErrors.NotFound("Data was not found " + request.Id));
        await unitOfWork.WriteRepositoryFor<AccountHolder>().DeleteAsync(accountHolderInDb);
        await unitOfWork.CommitAsync(cancellationToken);
        return ResponseWrapper<int>.Success(accountHolderInDb.Id, "Account Holder deleted");
    }
}