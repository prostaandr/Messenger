using Messenger.Data.Interfaces;
using Messenger.Domain;
using Messenger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public UserService(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<User> GetUser(int id)
        {
            return await _userRepository.Get(id);
        }


        public async Task<List<User>> GetInterlocutors(int id)
        {
            return await _userRepository.GetInterlocutors(id);
        }

        public async Task UpdateStatus(User user, UserStatus status)
        {
            await _userRepository.UpdateStatus(user, status);
        }

        public async Task SendUserMessage(UserMessage userMessage)
        {
            await _messageRepository.SendUserMessage(userMessage);
        }

        public async Task<UserMessage> GetLastUserMessage(int senderId, int reciverId)
        {
            return await _messageRepository.GetLastUserMessage(senderId, reciverId);
        }

        public async Task<List<UserMessage>> GetUserMessages(int senderId, int reciverId)
        {
            return await _messageRepository.GetUserMessages(senderId, reciverId);   
        }
    }
}
