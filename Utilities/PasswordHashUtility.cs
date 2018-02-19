using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HonestProject.Utilities
{
    public class PasswordHashUtility : IPasswordHashUtility
    {
        public string CalculateHash(string input)
        {
            byte[] salt = GenerateSalt(16);

            byte[] bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(bytes)}";
        }

        public bool CheckMatch(string hash, string input)
        {
            try
            {
                var parts = hash.Split(':');

                var salt = Convert.FromBase64String(parts[0]);

                var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

                return parts[1].Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }

        private byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return salt;
        }
    }
}