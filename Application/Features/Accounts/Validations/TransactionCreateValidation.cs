using Application.Features.Accounts.Commands;
using FluentValidation;

namespace Application.Features.Accounts.Validations;

public class TransactionCreateValidation : AbstractValidator<CreateTransactionCommand>
{
    public TransactionCreateValidation()
    {
        RuleFor(x => x.Transaction.AccountId).NotEmpty();
        RuleFor(x => x.Transaction.Amount).GreaterThan(0);
        RuleFor(x => x.Transaction.Type).IsInEnum();
    }
}