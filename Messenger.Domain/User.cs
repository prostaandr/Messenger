using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Icon { get; set; }
        public string Email { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
        public List<UserMessage> UserMessagesAsAddresser { get; set; } = new List<UserMessage>();
        public List<UserMessage> UserMessagesAsAddressee { get; set; } = new List<UserMessage>();
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<GroupMessage> GroupMessagesAsAddresser { get; set; } = new List<GroupMessage>();
    }
}
