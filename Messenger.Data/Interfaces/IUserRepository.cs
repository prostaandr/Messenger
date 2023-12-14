using Messenger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Get(int id);
        Task Update(User user);
        Task Create(User user);
    }
}
