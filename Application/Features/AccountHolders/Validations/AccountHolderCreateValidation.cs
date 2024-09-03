using Application.Features.AccountHolders.Commands;
using FluentValidation;

namespace Application.Features.AccountHolders.Validations;

public class AccountHolderCreateValidation : AbstractValidator<CreateAccountHolderCommand>
{
    public AccountHolderCreateValidation()
    {
        RuleFor(x => x.CreateAccountHolder.FirstName).NotEmpty();
        RuleFor(x => x.CreateAccountHolder.DateOfBirth).NotEmpty();
        RuleFor(x => x.CreateAccountHolder.ContactNumber).NotEmpty();
    }
}