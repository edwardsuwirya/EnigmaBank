using Application.Features.Accounts.Commands;
using FluentValidation;

namespace Application.Features.Accounts.Validations;

public class AccountCreateValidation : AbstractValidator<CreateAccountCommand>
{
    public AccountCreateValidation()
    {
        RuleFor(x => x.CreateAccount.AccountHolderId).NotEmpty();
        RuleFor(x => x.CreateAccount.Type).IsInEnum();
    }
}