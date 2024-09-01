using Application.Features.Accounts.Commands;
using Common.Exceptions;
using Common.Requests;
using FluentValidation;

namespace Application.Validations;

public class AccountCreateValidation : AbstractValidator<CreateAccountCommand>
{
    public AccountCreateValidation()
    {
        RuleFor(x => x.CreateAccount.AccountHolderId).NotEmpty();
        RuleFor(x => x.CreateAccount.Type).IsInEnum();
    }
}