namespace Common.Responses;

public class AuthenticationResponse
{
    public int Id { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}