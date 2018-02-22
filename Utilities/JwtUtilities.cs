using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HonestProject.Utilities
{

    public class JwtUtilities : IJwtUtilities
    {
        IConfiguration configuration;
        public JwtUtilities(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenereateJwtToken(DataModels.User user)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, user.EmailAddress), new Claim(ClaimTypes.Role, user.Role.Name) };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: "yourdomain.com",
            audience: "yourdomain.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(DataModels.User user)
        {
            byte[] salt = GenerateSalt(16);

            byte[] bytes = KeyDerivation.Pbkdf2(Guid.NewGuid().ToString(), salt, KeyDerivationPrf.HMACSHA512, 10000, 16);
            string refreshToken = Convert.ToBase64String(bytes);
            return refreshToken.Replace("+", string.Empty)
            .Replace("=", "")
            .Replace("/", "")
            .Replace(":","");
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