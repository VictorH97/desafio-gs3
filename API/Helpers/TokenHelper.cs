using API.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _configuration;

        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AccessToken GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Username),
                new Claim("Email", user.Email),
            };

            var expires = DateTime.Now.AddHours(6);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value!));

            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                claims: claims,
                expires: expires
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

            return new AccessToken()
            {
                Token = token,
                ExpiresIn = expires
            };
        }
    }
}
