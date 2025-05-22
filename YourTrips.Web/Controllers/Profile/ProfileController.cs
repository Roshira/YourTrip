using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.DTOs.Auth;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces;

namespace YourTrips.Web.Controllers.Profile
{
    [ApiController]
    [Route("api/[controller]")] // Base route for all actions in this controller
    public class ProfileController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly IRewriteUserName _rewriteUserName;

        public ProfileController(UserManager<User> userManager, IRewriteUserName rewrite)
        {
            _userManager = userManager;
            _rewriteUserName = rewrite;
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
            var result = await _rewriteUserName.RewriteUserNameAsync(User, userName);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
