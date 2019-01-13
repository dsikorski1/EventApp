using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Infrastructure.DTO
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public string CreatedAt { set; get; }

        public AccountDto()
        {
        }
    }
}
