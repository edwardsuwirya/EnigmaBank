using Common.Exceptions;
using Common.Requests;
using FluentValidation;

namespace Application.Validations;

public class AccountCreateValidation : AbstractValidator<CreateAccount>
{
    public AccountCreateValidation()
    {
        RuleFor(x => x.AccountHolderId).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
    }
}