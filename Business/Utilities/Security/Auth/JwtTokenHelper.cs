using Business.Utilities.Security.Auth.Interface;
using Core.Utilities;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Utilities.Security.Auth
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly IConfiguration _configuration;

        public JwtTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(User user, string refreshToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenOptions:SecurityKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.UtcNow.ToTimeZone().AddHours(1);

            var securityToken = new JwtSecurityToken(
                audience: _configuration["TokenOptions:Audience"],
                issuer: _configuration["TokenOptions:Issuer"],
                claims: SetClaims(user),
                expires: expirationDate,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenInstance = new Token(tokenHandler.WriteToken(securityToken), expirationDate, refreshToken);

            return tokenInstance;
        }

        private static IEnumerable<Claim> SetClaims(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Actor, user.Role.RoleName.ToString())

        };

            return claims;
        }
    }
}
