using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Infrastructure.Commands.Users
{
    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
