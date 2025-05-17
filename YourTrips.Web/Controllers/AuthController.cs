using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YourTrips.Core.DTOs.Auth;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Services;

namespace YourTrips.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Base route for all actions in this controller
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        public AuthController(
            IAuthService authService,
            UserManager<User> userManager

            )
        {
            _authService = authService;
            _userManager = userManager;
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registerDto">User registration data</param>
        /// <returns>Registration result</returns>
        [HttpPost("register")]
        [AllowAnonymous] // Allows access without authentication
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result); // Returns 400 with error details
            }
            return Ok(result); // Returns 200 with success message
        }


        /// <summary>
        /// Confirms user's email address
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="token">Confirmation token</param>
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest("User ID and token are required.");
            }

            if (!Guid.TryParse(userId, out _))
            {
                return BadRequest("Invalid User ID format.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // Don't reveal whether user exists
                return BadRequest("Unable to confirm email.");
            }

            token = Uri.UnescapeDataString(token); // URL decode the token

            var result = await _userManager.ConfirmEmailAsync(user, token);
           
            if (!result.Succeeded)
            {
                return BadRequest("Email confirmation failed. The link might be invalid or expired.");
            }

            return Redirect($"https://192.168.0.104:3000/email-confirm?token={Uri.EscapeDataString(token)}&userId={user.Id}");
        }

        [HttpGet("emailConfirmed")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmed(string token, string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest("User ID and token are required.");
            }

            if (!Guid.TryParse(userId, out _))
            {
                return BadRequest("Invalid User ID format.");
            }

            var user = await _userManager.FindByIdAsync(userId);
           
            if (user == null)
            {
                // Don't reveal whether user exists
                return BadRequest("Unable to confirm email.");
            }
            if (user.EmailConfirmed == true)
            {
                return BadRequest("User already registered");
            }
            token = Uri.UnescapeDataString(token); // URL decode the token

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest("Email confirmation failed. The link might be invalid or expired.");
            }

            return Ok(true);
        }
        /// <summary>
        /// Gets current authenticated user's information
        /// </summary>
        [HttpGet("current")]
        [Authorize] // IMPORTANT: Requires valid authentication cookie
        public async Task<IActionResult> GetCurrentUser()
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
            });
        }
    }
}
