using Application.Repositories;
using Common.Exceptions;
using Common.Requests;
using Common.Wrapper;
using Domain;
using Domain.Extensions;
using MediatR;

namespace Application.Features.AccountHolders.Commands;

public class UpdateAccountHolderCommand : IRequest<ResponseWrapper<int>>
{
    public UpdateAccountHolder UpdateAccountHolder { get; set; }
}

public class UpdateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<UpdateAccountHolderCommand, ResponseWrapper<int>>
{
    public async Task<ResponseWrapper<int>> Handle(UpdateAccountHolderCommand request,
        CancellationToken cancellationToken)
    {
        var accountHolderInDb =
            await unitOfWork.ReadRepositoryFor<AccountHolder>().GetByIdAsync(request.UpdateAccountHolder.Id);
        if (accountHolderInDb is null)
            return new ResponseWrapper<int>().Fail(AppError.NotFound(
                request.UpdateAccountHolder.Id));
        var updatedAccountHolder = accountHolderInDb.Update(
            request.UpdateAccountHolder.FirstName,
            request.UpdateAccountHolder.LastName,
            request.UpdateAccountHolder.ContactNumber,
            request.UpdateAccountHolder.Email);

        await unitOfWork.WriteRepositoryFor<AccountHolder>().UpdateAsync(updatedAccountHolder);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ResponseWrapper<int>().Success(updatedAccountHolder.Id, "Account Holder updated");
    }
}