﻿using Messenger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Dto
{
    public class UserDto
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
