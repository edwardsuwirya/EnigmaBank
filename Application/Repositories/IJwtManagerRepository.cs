using Domain;

namespace Application.Repositories;

public interface IJwtManagerRepository
{
    string GenerateAccessToken(string id);
    UserRefreshToken GenerateRefreshToken();
}