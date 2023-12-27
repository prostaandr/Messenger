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
        Task<List<User>> GetInterlocutors(int id);
        Task UpdateStatus (User user, UserStatus status);
        Task SendUserMessage(UserMessage userMessage);
        Task<UserMessage> GetLastUserMessage(int senderId, int reciverId);
        Task<List<UserMessage>> GetUserMessages(int senderId, int reciverId);
    }
}
