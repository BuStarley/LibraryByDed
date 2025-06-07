using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Command.Register;

public record RegisterUserCommand(
    string UserName,
    string PhoneNumber,
    string Password) : IRequest<Guid>;