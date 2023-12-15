using Messenger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST: Account/Login
        [HttpPost("Login")]
        public IActionResult Login (string login, string password)
        {
            var user = _accountService.Login(login, password);
            if (user != null)
            {
                return Ok("1");
            }
            return Ok("2");
        }

    }
}
