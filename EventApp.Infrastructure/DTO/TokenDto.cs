namespace EventApp.Infrastructure.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public string Role { get; set; }

        public TokenDto()
        {    
        }

        public TokenDto(string token, long expires, string role)
        {    
            Token = token;
            Expires = expires;
            Role = role;
        }
    }
}
