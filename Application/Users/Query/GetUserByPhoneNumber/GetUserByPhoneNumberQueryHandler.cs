using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query.GetUserByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler(
    IUserRepository repository,
    IMapper mapper)
    : IRequestHandler<GetUserByPhoneNumberQuery, UserDto>
{
    public async Task<UserDto> Handle(
        GetUserByPhoneNumberQuery request, 
        CancellationToken ct)
    {
        var user = await repository.GetByPhoneNumberAsync(request.PhoneNumber, ct);
        return mapper.Map<UserDto>(user);
    }
}
