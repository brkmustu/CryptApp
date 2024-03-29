﻿using System.Security.Cryptography;
using System.Text;

namespace Core.Shared.Security;

/// <summary>
/// Sennedjem FW'den alınmıştır :)
/// </summary>
public static class HashingExtensions
{
    public static (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(this string password)
    {
        using (var hmac = new HMACSHA512())
        {
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return (passwordHash, passwordSalt);
        }
    }

    public static bool VerifyPasswordHash(this string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
