using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourTrips.Core.DTOs.Auth;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Web.Controllers.Profile
{
    [ApiController]
    [Route("api/[controller]")] // Base route for all actions in this controller
    public class ProfileController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly IRewriteUserName _rewriteUserName;
        private readonly YourTripsDbContext _dbContext;

        public ProfileController(UserManager<User> userManager, IRewriteUserName rewrite, YourTripsDbContext context)
        {
            _userManager = userManager;
            _rewriteUserName = rewrite;
            _dbContext = context;
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
            var userRoles = await _dbContext.UserRoles
    .Where(ur => ur.UserId == user.Id)
    .ToListAsync();

            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

            var role = await _dbContext.Roles
                .Where(r => roleIds.Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();
            return Ok(new
            {
                user.Email,
                user.UserName,
                user.CreatedAt,
                role
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
