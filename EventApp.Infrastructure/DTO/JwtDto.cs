using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Infrastructure.DTO
{
    public class JwtDto
    {
        public string Token { get; set; }
        public long Expires { get; set; }

        public JwtDto()
        { 
        }

        public JwtDto(string token, long expires)
        {
            Token = token;
            Expires = expires;
        }
    }
}
