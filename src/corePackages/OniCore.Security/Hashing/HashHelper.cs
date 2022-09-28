using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Security.Hashing
{
    public static class HashHelper
    {
        public static void CreateHash(string plainText, out byte[] hash, out byte[] hashSalt)
        {
            using HMACSHA512 hmac = new();

            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            hashSalt = hmac.Key;
        }

        public static bool VerifyHash(string plainText, byte[] hash, byte[] hashSalt)
        {
            using HMACSHA512 hmac = new(hashSalt);

            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainText));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != hash[i])
                    return false;

            return true;
        }
    }
}
