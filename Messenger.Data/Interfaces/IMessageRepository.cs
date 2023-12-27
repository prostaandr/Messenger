using Messenger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Data.Interfaces
{
    public interface IMessageRepository
    {
        Task SendUserMessage(UserMessage userMessage);
        Task<UserMessage> GetLastUserMessage(int senderId, int reciverId);
        Task<List<UserMessage>> GetUserMessages(int senderId, int reciverId);
    }
}
