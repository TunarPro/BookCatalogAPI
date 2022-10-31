using BookCatalogLibrary.Models;
using BookCatalogLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateRequest request)
        {
            var response = await _userService.Authenticate(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _userService.RegisterUser(request);
            return Ok(new { message = "Registration completed successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUser(id);
            return Ok(user);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRequest request)
        {
            if (User.Claims.SingleOrDefault(x => x.Type == "UserId")?.Value != id.ToString())
            {
                return Forbid("You can only update your own account");
            }

            await _userService.UpdateUser(id, request);
            return Ok(new { message = "User updated successfully" });
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (User.Claims.SingleOrDefault(x => x.Type == "UserId")?.Value != id.ToString())
            {
                return Forbid("You can only delete your own account");
            }

            await _userService.DeleteUser(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
