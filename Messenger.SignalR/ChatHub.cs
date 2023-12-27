using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Data;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.SignalR
{
    public class ChatHub : Hub
    {
        //public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
        private readonly IUserService _userService;

        public ChatHub(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        public async Task PrivateSend(string message, string reciver)
        {
            var name = Context.User.Identity.Name;
            await Clients.User(reciver).SendAsync("SendPrivateReceive", message, false);
            await Clients.Caller.SendAsync("SendPrivateReceive", message, true);
        }

        public async override Task OnConnectedAsync()
        {
            var id = Context.User.Identity.Name;
            var user = await _userService.GetUser(Convert.ToInt32(id));
            await _userService.UpdateStatus(user, Domain.UserStatus.Online);
            await Clients.All.SendAsync("OnConnectedRecive", id);

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.User.Identity.Name;
            var user = await _userService.GetUser(Convert.ToInt32(id));
            await _userService.UpdateStatus(user, Domain.UserStatus.Offline);
            await Clients.All.SendAsync("OnDisconnectedRecive", id);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
