using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QUS.Auth.Application.Interfaces;
using QUS.Auth.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Services
{
    public class TokenJWT : ITokenJWT
    {
        private readonly IConfiguration _configuration;

        public TokenJWT(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> TokenGenerate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretKey"]!);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.Entrada),
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Task.FromResult(jwt);
        }
    }
}
