using Application.Interfaces;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public Task AddAsync(User user, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByPhoneNumberAsync(string PhoneNumber, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
