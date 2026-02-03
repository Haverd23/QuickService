using QUS.Auth.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Services
{
    public class PasswordEncryption : IPasswordEncryption
    {
        public string PasswordHash(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }
        public bool PasswordCheck(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var storedPasswordHash = Convert.FromBase64String(parts[1]);
            using (var hmac = new HMACSHA256(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedPasswordHash);
            }


        }
    }
}
