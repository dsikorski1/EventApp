using System;

namespace EventApp.Infrastructure.Commands.Users
{
    public class RegisterUser
    {
        public Guid Id { get; protected set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }

        public RegisterUser()
        {
            Id = Guid.NewGuid();
        }
    }
}
