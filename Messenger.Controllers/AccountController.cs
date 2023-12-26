using Messenger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Messenger.Dto;
using Messenger.Domain;
using Messenger.Services;

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
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _accountService.Login(request.Login, request.Password);
            if (user == null)
            {
                return BadRequest();
            }

            var token = user.Token;

            var response = new
            {
                token = user.Token
            };

            return Ok(token);
        }

        // POST: Account/Registration
        [HttpPost("Registration")]
        public async Task Registration(User user)
        {
            await _accountService.Registration(user);
        }

    }
}
