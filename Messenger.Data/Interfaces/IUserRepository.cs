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
        Task<User> Get(string login, string password);
        Task<List<User>> GetInterlocutors(int id);
        Task Update(User user);
        Task Create(User user);
        Task UpdateStatus(User user, UserStatus status);
    }
}
