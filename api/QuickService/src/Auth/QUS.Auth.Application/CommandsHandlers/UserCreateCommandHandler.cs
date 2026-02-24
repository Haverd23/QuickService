using QUS.Auth.Application.Commands;
using QUS.Auth.Application.Interfaces;
using QUS.Auth.Domain.Interfaces;
using QUS.Auth.Domain.Models;
using QUS.Core.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.CommandsHandlers
{
    public class UserCreateCommandHandler : ICommandHandler<UserCreateCommand, Guid>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordEncryption _passwordEncryption;
        public UserCreateCommandHandler(IAuthRepository authRepository, IPasswordEncryption passwordEncryption)
        {
            _authRepository = authRepository;
            _passwordEncryption = passwordEncryption;
        }

        public async Task<Guid> HandleAsync(UserCreateCommand command)
        {
            
           var emailExists = await _authRepository.GetByEmailAsync(command.Email);
           if (emailExists != null)
              {
                throw new Exception("Email already exists");
            }
           var senhaHash = _passwordEncryption.PasswordHash(command.Password);
           var user = new User(Guid.NewGuid(),command.Email, senhaHash);
           await _authRepository.AddAsync(user);
           var success = await _authRepository.UnitOfWork.Commit();
            if (!success)
            {
              throw new Exception("Error saving user");
            }
            return user.Id;


        }
    }
}
