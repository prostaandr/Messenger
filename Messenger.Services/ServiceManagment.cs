
using Messenger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Services
{
    public class ServiceManagment
    {
        private IUserService _userService;

        public ServiceManagment(IUserService userService)
        {
            _userService = userService;
        }

        public IUserService GetUserService()
        {
            return _userService;
        }
    }
}
