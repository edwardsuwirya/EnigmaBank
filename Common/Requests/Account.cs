using Common.Enums;

namespace Common.Requests;

public record CreateAccount(
    int AccountHolderId,
    decimal Balance,
    AccountType Type);

public record TransactionRequest(int AccountId, decimal Amount, TransactionType Type);