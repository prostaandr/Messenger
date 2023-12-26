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
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenParameters _tokenParameters;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tokenParameters = new TokenParameters();
        }

        public async Task<UserDto> Login(string login, string password)
        {
            var user = await _userRepository.Get(login);

            if (user == null) return null;
            var token = user.GenerateJwtToken(_tokenParameters);
            return new UserDto { User = user, Token = token };
        }

        public async Task Registration(User user) => await _userRepository.Create(user);
    }
}
