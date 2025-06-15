using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions;

[Serializable]
public class IncorrectPassword : Exception
{
    public IncorrectPassword()
    {
    }

    public IncorrectPassword(string? message) : base(message)
    {
    }

    public IncorrectPassword(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
