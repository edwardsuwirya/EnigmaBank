using Common.Enums;

namespace Common.Responses;

public class AccountTransactionsResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public int AccountHolderId { get; set; }
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
    public AccountType Type { get; set; }

    public List<TransactionResponse> Transactions { get; set; }
}