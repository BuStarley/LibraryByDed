using Application.Interfaces;
using Domain.Entity;
using MediatR;

namespace Application.Users.Command.Register;

public class RegisterUserCommandHandler(
    IUserRepository repository,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(
        RegisterUserCommand request,
        CancellationToken ct)
    {
        var user = User.Create(
            request.UserName,
            request.PhoneNumber,
            passwordHasher.Hash(request.Password));

        await repository.AddAsync(user);
        return user.Id;
    }
}
