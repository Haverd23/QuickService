using QUS.Service.Application.Interfaces;
using QUS.Users.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Service.Application.Service
{
    public class GetUserIdService : IGetUserIdService
    {
        private readonly IUserRepository _userRepository;
        public GetUserIdService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Guid> GetUserId(Guid Id)
        {
            var userID = await _userRepository.GetById(Id);
            return userID.Id;
        }
    }
}
