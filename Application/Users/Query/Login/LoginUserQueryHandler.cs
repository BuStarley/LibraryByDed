using Application.Dto;
using Application.Exceptions;
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
                var message = "There is no account with this phone number";
                logger.LogTrace($"{message} number={request.PhoneNumber}");
                throw new Exception(message);
            }

            if (!hasher.Verify(request.Password, user.PasswordHash))
            {
                var message = "Incorrect password";
                logger.LogWarning($"Incorrect password id={user.Id}");
                throw new IncorrectPassword(message);
            }

            return new(user.Id, "");
        }
        catch (Exception ex)
        {
            var message = "Connection problems";
            logger.LogError(message);
            throw new AuthenticationException(message); ;
        }
    }
}
