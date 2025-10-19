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

        public AccessToken GenerateJwtToken(Usuario user)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Nome", user.Nome),
                new Claim("Email", user.Email)
            };

            if (user.Perfil != null)
            {
                foreach (var permissao in user.Perfil.Permissoes.Split(','))
                {
                    claims.Add(new Claim(ClaimTypes.Role, permissao.Trim()));
                }
            }

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

        public string GenerateHash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        
        public bool VerifyHash(string input, string hash)
        {
            var inputHash = GenerateHash(input);
            return inputHash == hash;
        }
    }
}
