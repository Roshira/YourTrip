using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.DTOs;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces;

namespace YourTrips.Infrastructure.Services.ProfileServices
{
    /// <summary>
    /// Service for handling username modification operations
    /// </summary>
    public class RewriteUserName : IRewriteUserName
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the RewriteUserName service
        /// </summary>
        /// <param name="userManager">ASP.NET Core Identity UserManager for user operations</param>
        public RewriteUserName(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Updates the username for the currently authenticated user
        /// </summary>
        /// <param name="userClaims">ClaimsPrincipal representing the current user</param>
        /// <param name="newUserName">New username to set</param>
        /// <returns>
        /// ResultDto indicating success or failure of the operation,
        /// with appropriate error messages if applicable
        /// </returns>
        public async Task<ResultDto> RewriteUserNameAsync(ClaimsPrincipal userClaims, string newUserName)
        {
            // Get the current user from the claims principal
            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null)
                return ResultDto.Fail("User not found for the current session.");

            // Check if the new username is already taken
            var existingUser = await _userManager.FindByNameAsync(newUserName);
            if (existingUser != null)
                return ResultDto.Fail($"Username {newUserName} already exists");

            // Update the username and normalized username
            user.UserName = newUserName;
            user.NormalizedUserName = _userManager.NormalizeName(newUserName);

            // Persist the changes
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return ResultDto.Fail("Failed to update username", result.Errors.Select(e => e.Description));

            return ResultDto.Success("UserName successfully updated");
        }
    }
}