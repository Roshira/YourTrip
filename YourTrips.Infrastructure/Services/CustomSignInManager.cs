using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;

namespace YourTrips.Infrastructure.Services
{
    public class CustomSignInManager : SignInManager<User>
    {
        public CustomSignInManager(
            UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userNameOrEmail, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManager.FindByEmailAsync(userNameOrEmail);
            if (user == null)
            {
                user = await UserManager.FindByNameAsync(userNameOrEmail);
            }

            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await base.PasswordSignInAsync(user.UserName, password, isPersistent, lockoutOnFailure);
        }
    }
}
