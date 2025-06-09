using Application.Dto;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query.GetUserByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IRequest<UserDto>;
