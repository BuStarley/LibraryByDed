using Domain.Entity;

namespace Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateRefreshToken();
    string GenerateToken(User user);
    int GetTokenExpiry();
}