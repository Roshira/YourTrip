using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.DTOs.Auth;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Services;
using YourTrips.Infrastructure.Services;

namespace YourTrips.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthController(IAuthService authService,
            UserManager<User> userManager,
            IJwtTokenGenerator jwtTokenGenerator
            )


        {
            _userManager = userManager;
            _authService = authService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
      
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("Invalid user ID");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return BadRequest("Email confirmation failed");

            // Генеруємо JWT тільки після підтвердження
            var jwtToken = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new
            {
                message = "Email confirmed successfully",
                token = jwtToken
            });
        }
    }
}
