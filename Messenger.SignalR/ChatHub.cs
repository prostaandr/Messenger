using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Data;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.SignalR
{
    public class ChatHub : Hub
    {
        //public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();

        public async Task PrivateSend(string message, string name)
        {
            await Clients.All.SendAsync("Receive", message, name);
        }
    }
}
