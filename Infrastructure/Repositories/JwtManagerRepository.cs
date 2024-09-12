using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Repositories;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Utilities;

namespace Infrastructure.Repositories;

public class JwtManagerRepository(IConfiguration configuration) : IJwtManagerRepository
{
    public string GenerateAccessToken(string id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var issuer = configuration["Authentication:Schemes:Bearer:ValidIssuer"];
        var accessTokenExpiryMinutes = configuration["Authentication:Schemes:Bearer:AccessTokenExpiryMinutes"];
        double.TryParse(accessTokenExpiryMinutes, result: out var expiryMinute);
        var secret = KeyHandler.GetSigningKey(configuration, issuer).FirstOrDefault();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("Id", id),
            ]),
            Issuer = issuer,
            Expires = DateTime.UtcNow.AddMinutes(expiryMinute),
            SigningCredentials =
                new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public UserRefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        RandomNumberGenerator.Create().GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);
        var refreshTokenExpiryMinutes = configuration["Authentication:Schemes:Bearer:RefreshTokenExpiryMinutes"];
        double.TryParse(refreshTokenExpiryMinutes, result: out var expiryMinute);
        return new UserRefreshToken
        {
            RefreshToken = refreshToken,
            Expires = DateTime.UtcNow.AddMinutes(expiryMinute),
        };
    }
}