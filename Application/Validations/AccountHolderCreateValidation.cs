using Application.Features.AccountHolders.Commands;
using Application.Features.Accounts.Commands;
using Common.Exceptions;
using Common.Requests;
using FluentValidation;

namespace Application.Validations;

public class AccountHolderCreateValidation : AbstractValidator<CreateAccountHolderCommand>
{
    public AccountHolderCreateValidation()
    {
        RuleFor(x => x.CreateAccountHolder.FirstName).NotEmpty();
        RuleFor(x => x.CreateAccountHolder.DateOfBirth).NotEmpty();
        RuleFor(x => x.CreateAccountHolder.ContactNumber).NotEmpty();
    }
}