using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.SignalR
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message, string name);
    }
}
