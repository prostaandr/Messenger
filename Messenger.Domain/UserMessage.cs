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
        public int AddresserId { get; set; }
        public User Addresser { get; set; }
        public int AddresseeId { get; set; }
        public User Addressee { get; set; }
    }
}
