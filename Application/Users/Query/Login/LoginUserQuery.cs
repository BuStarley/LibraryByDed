using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query.Login;

public record LoginUserQuery(
    string PhoneNumber,
    string Password) 
    : IRequest<AuthenticationResult>;
