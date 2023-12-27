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
    public class MessageRepository : IMessageRepository
    {
        private readonly MessengerContext _db;

        public MessageRepository(MessengerContext db)
        {
            _db = db;
        }

        public async Task<UserMessage> GetLastUserMessage(int senderId, int reciverId)
        {
            return await _db.UserMessages.Include(um => um.Message).Where(um => um.SenderId == senderId && um.ReciverId == reciverId).LastOrDefaultAsync();
        }

        public async Task<List<UserMessage>> GetUserMessages(int senderId, int reciverId)
        {
            var userMessages = await _db.UserMessages.Include(m => m.Message).Where(m => m.SenderId == senderId && m.ReciverId == reciverId).ToListAsync();
            userMessages.AddRange(await _db.UserMessages.Include(m => m.Message).Where(m => m.SenderId == reciverId && m.ReciverId == senderId).ToListAsync());

            return userMessages;
        }

        public async Task SendUserMessage(UserMessage userMessage)
        {
            await _db.UserMessages.AddAsync(userMessage);
            await _db.SaveChangesAsync();
        }
    }
}
