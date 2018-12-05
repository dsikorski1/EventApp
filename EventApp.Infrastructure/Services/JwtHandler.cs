using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventApp.Infrastructure.DTO;
using EventApp.Infrastructure.Extensions;
using EventApp.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventApp.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtDto CreateToken(Guid userId, string role)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);
            var jwt = CreateJwtToken(userId, role, now, expires);
            
            return new JwtDto(jwt, expires.ToTimestamp());
        }

        private string CreateJwtToken(Guid userId, string role, DateTime now, DateTime expires)
        {
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: CreateClaims(userId, role, now),
                notBefore: now,
                expires: expires,
                signingCredentials: CreateSigningCredentials()
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private Claim[] CreateClaims(Guid userId, string role, DateTime now)
        {
            return new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString())
            };
        }

        private SigningCredentials CreateSigningCredentials()
            => new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256);  
    }
}
