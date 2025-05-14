using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces;
using YourTrips.Core.DTOs.Auth;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Services;

namespace YourTrips.Infrastructure.Services.AuthServices
{
    /// <summary>
    /// Service handling user authentication and registration
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; // Required for cookie authentication
        private readonly IAppEmailSender _emailSender;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAppEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Registers a new user account
        /// </summary>
        /// <param name="registerDto">User registration data</param>
        /// <returns>Registration result with success status and message</returns>
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Email already exists"
                };
            }

            // Create new user entity
            var newUser = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName, // Important: UserName must be unique
                CreatedAt = DateTime.UtcNow,
            };

            // Fallback to email if username not provided
            if (string.IsNullOrEmpty(newUser.UserName))
            {
                newUser.UserName = registerDto.Email;
            }

            // Create user with password
            var result = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            // Generate email confirmation token
            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            // WARNING: Don't use local IP in production!
            var confirmationLink = $"https://192.168.0.103:7271/api/auth/confirm-email?userId={newUser.Id}&token={Uri.EscapeDataString(emailToken)}";

            // Create HTML email message
            var htmlMessage = $"<p>Welcome to YourTrips!</p><p>Click the link below to confirm your email:</p><a href='{confirmationLink}'>Confirm Email</a>";

            // Send confirmation email
            await _emailSender.SendEmailAsync(newUser.Email, "Confirm your email", htmlMessage);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Registration successful. Please check your email to confirm your account."
                // Don't return Email/UserName here to prevent usage before confirmation
            };
        }

    }
}