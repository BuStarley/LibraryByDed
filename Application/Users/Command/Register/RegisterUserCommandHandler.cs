using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Command.Register;

public class RegisterUserCommandHandler(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    ILogger logger)
    : IRequestHandler<RegisterUserCommand, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(
        RegisterUserCommand request, 
        CancellationToken ct)
    {
        try
        {
            if (await repository.ExistsByPhoneNumber(request.PhoneNumber, ct))
            {
                var message = "The phone number is busy";
                logger.LogWarning(message);
                throw new AuthenticationException(message);
            }

            var passwordHash = passwordHasher.GetHash(request.Password);

            var user = User.Create(
                request.UserName,
                request.PhoneNumber,
                passwordHash
                );

            await repository.AddAsync(user, ct);
            logger.LogInformation($"Register new User:" +
                $" id={user.Id}" +
                $" name={user.UserName}" +
                $" number={user.PhoneNumber}");

            return new AuthenticationResult(user.Id, "");
        }
        catch ( Exception ex )
        {
            var message = "Connection problems";
            logger.LogError(message);
            throw new AuthenticationException(message);
        }
    }
}
