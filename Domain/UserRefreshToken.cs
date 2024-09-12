namespace Domain;

public class UserRefreshToken
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string RefreshToken { get; set; }

    public DateTime Expires { get; set; }
}