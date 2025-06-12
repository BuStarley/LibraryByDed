using Domain.Entity;

namespace Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    int GetTokenExpiry();
}