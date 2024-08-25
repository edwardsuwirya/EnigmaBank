namespace Domain;

public class AccountHolder : Person
{
    public string ContractNumber { get; set; }
    public string Email { get; set; }

    public List<Account> Accounts { get; set; }
}