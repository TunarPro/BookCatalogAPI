using BookCatalogLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookCatalogLibrary.Services
{
    public class JwtService : IJwtService
    {
        private readonly BookDBContext _context;
        public IConfiguration _config;

        public JwtService(BookDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Username", user.Username),
                    new Claim(ClaimTypes.GivenName, user.GivenName),
                    new Claim(ClaimTypes.Role, _context.Roles.SingleOrDefault(x => x.Id == user.RoleId).RoleName)
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
