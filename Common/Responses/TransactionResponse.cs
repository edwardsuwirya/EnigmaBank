using Common.Enums;

namespace Common.Responses;

public class TransactionResponse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Balance { get; set; }
    public DateTime Date { get; set; }
}