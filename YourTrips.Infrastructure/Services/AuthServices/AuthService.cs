using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces;
using YourTrips.Core.DTOs.Auth;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Services;

namespace YourTrips.Infrastructure.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAppEmailSender _emailSender;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IAppEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailSender = emailSender;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null) 
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Email already exists"
                };
            }
            var newUser = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                CreatedAt = DateTime.UtcNow,
                IconUrl = "https://w7.pngwing.com/pngs/184/113/png-transparent-user-profile-computer-icons-profile-heroes-black-silhouette-thumbnail.png"
            };
            var result = await _userManager.CreateAsync( newUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join( ", ", result.Errors.Select(e => e.Description)) 
                };
            }

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

             var confirmationLink = $"http://192.168.0.102:7271/api/auth/confirm-email?userId={newUser.Id}&token={Uri.EscapeDataString(emailToken)}";
            //var confirmationLink = $"http://192.168.0.102:7271/api/auth/confirm-email?userId={newUser.Id}&token={Uri.EscapeDataString(emailToken)}";
            var htmlMessage = $"<p>Welcome to YourTrips!</p><p>Click the link below to confirm your email:</p><a href='{confirmationLink}'>Confirm Email</a>";

            await _emailSender.SendEmailAsync(newUser.Email, "Confirm your email", htmlMessage);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Registration successful. Please check your email to confirm your account."
            };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid credentials"
                };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                Email = user.Email,
                UserName = user.UserName
            };

        }

       

    }
}
