using BookCatalogLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookCatalogLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly BookDBContext _context;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(BookDBContext context, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _context.Users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Username.ToLower() == model.Username.ToLower());


            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new Exception("Username or password is incorrect");

            AuthenticateResponse response = new()
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role?.RoleName,
                GivenName = user.GivenName
            };
            response.Token = _jwtService.GenerateToken(user);
            return response;
        }

        public async Task<User> RegisterUser(RegisterRequest request)
        {
            if (_context.Users.Any(x => x.Username == request.Username))
                throw new Exception("Username '" + request.Username + "' already exists");

            if (!_context.Roles.Any(x => x.RoleName.ToLower() == request.Role.ToLower()))
                throw new Exception("Invalid Role name");

            var user = new User()
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = _context.Roles.SingleOrDefault(x => x.RoleName.ToLower() == request.Role.ToLower())?.Id,
                GivenName = request.GivenName
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = GetUser(id).Result;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return user;
        }

        public async Task<User> UpdateUser(int id, UpdateRequest request)
        {
            var user = GetUser(id).Result;

            if (request.Username != user.Username && _context.Users.Any(x => x.Username == request.Username))
                throw new Exception("Username '" + request.Username + "' already exists");

            if (!_context.Roles.Any(x => x.RoleName == request.Role))
                throw new Exception("Invalid Role name");

            if (!string.IsNullOrEmpty(request.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Username = request.Username;
            user.GivenName = request.GivenName;

            if (_httpContextAccessor.HttpContext != null)
            {
                var currentRole = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                var currentUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserId")?.Value);

                if (currentRole == "Admin" && user.Id != currentUserId)
                {
                    user.RoleId = _context.Roles.SingleOrDefault(x => x.RoleName.ToLower() == request.Role.ToLower())?.Id;
                }
            }
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
