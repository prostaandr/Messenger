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
            return await _db.Users.Where(u => u.Id == id).Include(u => u.Groups).SingleOrDefaultAsync();
        }

        public async Task<User> Get(string login, string password)
        {
            return await _db.Users.Where(u => u.Login == login && u.Password == password).Include(u => u.Groups).SingleOrDefaultAsync();
        }

        public async Task<List<User>> GetInterlocutors(int id)
        {
            var user = await _db.Users.Include(u => u.UserMessagesAsSender).ThenInclude(um => um.Message).Include(u => u.UserMessagesAsReciver).ThenInclude(um => um.Message).Where(u => u.Id == id).FirstOrDefaultAsync();
            var reciver = new List<User>();
            foreach (var um in user.UserMessagesAsReciver)
            {
                if (um.ReciverId == id)
                    reciver.Add(await _db.Users.Include(u => u.UserMessagesAsSender).ThenInclude(um => um.Message).Include(u => u.UserMessagesAsReciver).ThenInclude(um => um.Message).Where(u => u.Id == um.SenderId).FirstOrDefaultAsync());
            }
            var sender = new List<User>();
            foreach (var um in user.UserMessagesAsSender)
            {
                if (um.SenderId == id)
                sender.Add(await _db.Users.Include(u => u.UserMessagesAsSender).ThenInclude(um => um.Message).Include(u => u.UserMessagesAsReciver).ThenInclude(um => um.Message).Where(u => u.Id == um.ReciverId).FirstOrDefaultAsync());
            }

            var general = reciver.Union(sender).ToList();

            return general;
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

        public async Task UpdateStatus(User user, UserStatus status)
        {
            user.Status = status;
            _db.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
