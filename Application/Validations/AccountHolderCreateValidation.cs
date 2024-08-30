using Common.Exceptions;
using Common.Requests;
using FluentValidation;

namespace Application.Validations;

public class AccountHolderCreateValidation : AbstractValidator<CreateAccountHolder>
{
    public AccountHolderCreateValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.ContactNumber).NotEmpty();
    }
}