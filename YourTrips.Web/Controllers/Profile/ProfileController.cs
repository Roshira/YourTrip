using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.DTOs.Auth;
using YourTrips.Core.Entities;

namespace YourTrips.Web.Controllers.Profile
{
    [ApiController]
    [Route("api/[controller]")] // Base route for all actions in this controller
    public class ProfileController : Controller
    {

        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("Profile")]
        [Authorize] // IMPORTANT: Requires valid authentication cookie
        public async Task<IActionResult> Profile()
        {
            // If request reaches here, user is authenticated
            var user = await _userManager.GetUserAsync(User); // Get full User object from DB

            if (user == null)
            {
                // Shouldn't happen if cookie is valid
                return Unauthorized(new { Message = "User not found for the current session." });
            }

            return Ok(new
            {
                user.Email,
                user.UserName,
                user.CreatedAt
            });
        }

        [HttpPut("RewriteUserName")]
        [Authorize]
        public async Task<IActionResult> RewriteUserName([FromQuery] string userName)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { Message = "User not found for the current session." });
            }

            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Message = $"Username {userName} already exists"
                });
            }

            user.UserName = userName;
            user.NormalizedUserName = _userManager.NormalizeName(userName);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Message = "Failed to update username",
                    Errors = result.Errors.Select(e => e.Description)
                });
            }

            return Ok(new
            {
                IsSuccess = true,
                Message = "UserName successfully updated"
            });
        }

    }
}
