using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.SignalR
{
    public class ChatHub : Hub
    {
        //public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();

        [Authorize]
        public async Task PrivateSend(string message, string reciver)
        {
            var name = Context.User.Identity.Name;
            await Clients.User(reciver).SendAsync("SendPrivateReceive", message, name );
            await Clients.Caller.SendAsync("SendPrivateReceive", message, name);
        }
    }
}
