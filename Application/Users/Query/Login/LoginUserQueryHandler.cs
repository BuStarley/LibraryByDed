using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query.Login;

public class LoginUserQueryHandler(
    IUserRepository repository,
    IPasswordHasher hasher,
    IJwtTokenService jwtTokenService,
    ILogger<LoginUserQueryHandler> logger)
    : IRequestHandler<LoginUserQuery, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(
        LoginUserQuery request, CancellationToken ct)
    {
        try
        {
            var user = await repository.GetByPhoneNumberAsync(request.PhoneNumber, ct);

            if (user == null)
            {
                logger.LogWarning("Login attempt for non-existent user: {PhoneNumber}", request.PhoneNumber);
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            if (!user.IsActive)
            {
                logger.LogWarning("Login attempt for inactive user: {UserId}", user.Id);
                throw new UnauthorizedAccessException("Account is inactive");
            }

            if (!hasher.Verify(request.Password, user.PasswordHash))
            {
                logger.LogWarning("Invalid password attempt for user: {UserId}", user.Id);
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = jwtTokenService.GenerateToken(user);
            logger.LogInformation("User {UserId} logged in successfully", user.Id);

            return new AuthenticationResult
            {
                Token = token,
                ExpiresIn = jwtTokenService.GetTokenExpiry(),
                UserId = user.Id
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during login for {PhoneNumber}", request.PhoneNumber);
            throw;
        }
    }
}
