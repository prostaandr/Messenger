using Messenger.Domain;
using Messenger.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> Login(string login, string password);
        Task Registration(User user);
    }
}
