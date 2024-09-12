namespace Domain;

public class UserAccessDummyDb
{
    public static List<UserAccess> Users =
    [
        new()
        {
            Id = 123, Password = "123456", Username = "Edo"
        }
    ];

    public static List<UserRefreshToken> UserRefreshTokens = [];
}