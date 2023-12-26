using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class UserMessage
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ReciverId { get; set; }
        public User Reciver { get; set; }
    }
}
