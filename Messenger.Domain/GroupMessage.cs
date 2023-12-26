using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
