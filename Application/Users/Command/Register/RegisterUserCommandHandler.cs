using Application.Exceptions;
using Application.Interfaces;
using Domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Command.Register;

public class RegisterUserCommandHandler(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    ILogger<RegisterUserCommandHandler> logger)
    : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(
        RegisterUserCommand request,
        CancellationToken ct)
    {
        try
        {
            logger.LogInformation("Starting registration for phone: {PhoneNumber}", request.PhoneNumber);

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
            {
                throw new ValidationException("Password must be at least 8 characters long");
            }

            var existingUser = await repository.GetByPhoneNumberAsync(request.PhoneNumber, ct);
            if (existingUser != null)
            {
                throw new ConflictException($"User with phone {request.PhoneNumber} already exists");
            }

            var user = User.Create(
                request.UserName,
                request.PhoneNumber,
                passwordHasher.Hash(request.Password));

            await repository.AddAsync(user, ct);
            logger.LogInformation("User registered successfully with ID: {UserId}", user.Id);

            return user.Id;
        }
        catch (Exception ex) when (ex is not ValidationException and not ConflictException)
        {
            logger.LogError(ex, "Error during registration for phone: {PhoneNumber}", request.PhoneNumber);
            throw new RegistrationException("Registration failed", ex);
        }
    }
}
