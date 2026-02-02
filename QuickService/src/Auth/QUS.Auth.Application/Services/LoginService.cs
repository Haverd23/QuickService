using QUS.Auth.Application.Interfaces;
using QUS.Auth.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Services
{
    public class LoginService : ILogin
    {
        private readonly IPasswordEncryption _passwordEncryption;
        private readonly ITokenJWT _tokenJWT;
        private readonly IAuthRepository _authRepository;
        
        public LoginService(IPasswordEncryption passwordEncryption, ITokenJWT tokenJWT, IAuthRepository authRepository)
        {
            _passwordEncryption = passwordEncryption;
            _tokenJWT = tokenJWT;
            _authRepository = authRepository;
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var user = await _authRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if(!_passwordEncryption.PasswordCheck(password, user.Password))
            {
                throw new Exception("Invalid password");
            }
            return await _tokenJWT.TokenGenerate(user);
        }
    }
}
