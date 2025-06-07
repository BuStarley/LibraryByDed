using Application.Interfaces;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
