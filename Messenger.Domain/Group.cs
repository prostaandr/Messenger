using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class Group
    {
        public int Id { get; set; }
        public string Icon { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        public List<GroupMessage> GroupMessagesAsAddressee { get; set; } = new List<GroupMessage>();
    }
}
