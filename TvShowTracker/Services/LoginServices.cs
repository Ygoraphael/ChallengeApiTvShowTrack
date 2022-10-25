using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TvShowTracker.Interfaces;
using System.Security.Claims;
using TvShowTracker.Model;
using TvShowTracker.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TvShowTracker.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public LoginServices(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        private async Task<string> GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<User> Autenticate(UserPostDTO userLogin)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLogin.Email && u.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
        public async Task<string> Login(UserPostDTO userLogin)
        {
            try
            {
                User user = await Autenticate(userLogin);
                if (user != null)
                {
                    string token = await GenerateToken(user);
                    return token;
                }
                return "";
            }
            catch
            {
                throw;
            }
        }
    }
}