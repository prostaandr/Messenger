using Messenger.Data.Interfaces;
using Messenger.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MessengerContext _db;

        public UserRepository(MessengerContext db)
        {
            _db = db;
        }

        public async Task<User> Get(int id)
        {
            return await _db.Users.Include(u => u.Groups).SingleOrDefaultAsync();
        }

        public async Task Update(User user)
        {
            _db.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task Create(User user)
        {
            _db.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}
