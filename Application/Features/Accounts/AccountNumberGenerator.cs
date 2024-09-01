namespace Application.Features.Accounts;

public static class AccountNumberGenerator
{
    public static string Generate() => DateTime.Now.ToString("yyMMddHHmmss");
}