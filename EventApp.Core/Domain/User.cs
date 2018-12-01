using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Core.Domain
{
    public class User : Entity
    {
        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string Firstname { get; protected set; }
        public string Lastname { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; protected set; }

        public User(Guid guid) : base(guid)
        {
        }
    }
}
