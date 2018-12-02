using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Core.Domain
{
    public class User : Entity
    {
        public string Email { get; protected set; }
        public string Firstname { get; protected set; }
        public string Lastname { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; protected set; }

        protected User() : base(Guid.NewGuid())
        {
        }

        public User(Guid id, string email, string firstname, string lastname, string password) : base(id)
        {
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Password = password;
        }
    }
}
