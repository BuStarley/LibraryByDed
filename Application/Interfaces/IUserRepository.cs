using Application.Dto;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken ct);
    Task<bool> ExistsByPhoneNumber(string phoneNumber, CancellationToken ct);
    Task<User> GetByPhoneNumberAsync(string PhoneNumber, CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
}
