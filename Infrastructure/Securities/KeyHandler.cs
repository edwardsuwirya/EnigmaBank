using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Utilities;

public class KeyHandler
{
    private const string KeySection = "Authentication:Schemes:Bearer:SigningKeys";
    private const string KeySectionIssuer = "Issuer";
    private const string KeySectionValue = "Value";

    public static IEnumerable<SecurityKey> GetSigningKey(IConfiguration configuration, String TheIssuer) =>
        GetSecurityKeys(configuration, TheIssuer);

    private static IEnumerable<SecurityKey> GetSecurityKeys(IConfiguration configuration, string issuer)
    {
        var signingKey = configuration
            .GetSection(KeySection)
            .GetChildren()
            .SingleOrDefault(key => key[KeySectionIssuer] == issuer);

        if (signingKey?[KeySectionValue] is { } secretKey)
        {
            yield return new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
        }
    }
}