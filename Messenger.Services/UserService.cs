using Messenger.Authentication;
using Messenger.Data;
using Messenger.Data.Interfaces;
using Messenger.Domain;
using Messenger.Dto;
using Messenger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenParameters _tokenParameters;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tokenParameters = new TokenParameters();
        }

        public async Task<UserDto> Login(int userId)
        {
            var user = await _userRepository.Get(userId);

            if (user == null) return null; 
            return new UserDto { User = user, Token = user.GenerateJwtToken(_tokenParameters) };
        }

        public async Task Registration(User user) => await _userRepository.Update(user);
    }
}
