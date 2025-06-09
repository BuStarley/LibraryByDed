using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Hashers;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName _hashAlgorithmName 
        = HashAlgorithmName.SHA256;
    private const char Delimiter = ';';
    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
        salt,
            Iterations,
            _hashAlgorithmName,
            KeySize);

        return string.Join(Delimiter,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash));
    }
}
