using Application.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext db/*, ILogger logger*/) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken ct)
    {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsByPhoneNumber(string phoneNumber, CancellationToken ct) =>
        await db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, ct) == null; 

    public async Task<User> GetByPhoneNumberAsync(string PhoneNumber, CancellationToken ct)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == PhoneNumber, ct);

        if (user == null)
        {
            var message = "Not found user";
            //logger.LogTrace(message);
            throw new NotFoundExceptionUser(message);
        }

        return user;
    }

    public async Task UpdateAsync(User user, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
