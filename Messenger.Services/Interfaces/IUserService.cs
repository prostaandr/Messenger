using Messenger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(int id);
    }
}
