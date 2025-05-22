using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces;

namespace YourTrips.Infrastructure.Services.ProfileServices
{
    public class RewriteUserName : IRewriteUserName
    {

        private readonly UserManager<User> _userManager;

        public RewriteUserName(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultDto> RewriteUserNameAsync(ClaimsPrincipal userClaims, string newUserName)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null)
                return ResultDto.Fail("User not found for the current session.");

            var existingUser = await _userManager.FindByNameAsync(newUserName);
            if (existingUser != null)
                return ResultDto.Fail($"Username {newUserName} already exists");

            user.UserName = newUserName;
            user.NormalizedUserName = _userManager.NormalizeName(newUserName);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return ResultDto.Fail("Failed to update username", result.Errors.Select(e => e.Description));

            return ResultDto.Success("UserName successfully updated");
        }

    }
}
