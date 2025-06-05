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
    /// <summary>
    /// Controller for managing user profile information.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRewriteUserName _rewriteUserName;
        private readonly YourTripsDbContext _dbContext;

        /// <summary>
        /// Constructor for ProfileController.
        /// </summary>
        /// <param name="userManager">ASP.NET Core Identity user manager.</param>
        /// <param name="rewrite">Service to rewrite the username.</param>
        /// <param name="context">Database context.</param>
        public ProfileController(UserManager<User> userManager, IRewriteUserName rewrite, YourTripsDbContext context)
        {
            _userManager = userManager;
            _rewriteUserName = rewrite;
            _dbContext = context;
        }

        /// <summary>
        /// Gets the profile information of the currently authenticated user.
        /// </summary>
        /// <returns>User profile data including email, username, creation date, and roles.</returns>
        /// <response code="200">Returns the user profile info.</response>
        /// <response code="401">User is not authorized or session is invalid.</response>
        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
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

        /// <summary>
        /// Changes the username of the currently authenticated user.
        /// </summary>
        /// <param name="userName">The new username to set.</param>
        /// <returns>Operation result indicating success or failure.</returns>
        /// <response code="200">Username was successfully changed.</response>
        /// <response code="400">Invalid username or update failed.</response>
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
