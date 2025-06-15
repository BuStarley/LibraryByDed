using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IPasswordHasher
{
    string GetHash(string password);

    bool Verify(string password, string passwordHash);
}
