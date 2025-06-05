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
    /// <summary>
    /// Custom implementation of <see cref="SignInManager{TUser}"/> to support login with either username or email.
    /// </summary>
    public class CustomSignInManager : SignInManager<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSignInManager"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="contextAccessor">Provides access to the current HTTP context.</param>
        /// <param name="claimsFactory">Factory to create a <see cref="System.Security.Claims.ClaimsPrincipal"/> from a user.</param>
        /// <param name="optionsAccessor">Provides access to identity options.</param>
        /// <param name="logger">Logger instance.</param>
        /// <param name="schemes">Authentication scheme provider.</param>
        /// <param name="confirmation">User confirmation logic.</param>
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

        /// <summary>
        /// Signs in a user using either their username or email address.
        /// </summary>
        /// <param name="userNameOrEmail">The username or email of the user.</param>
        /// <param name="password">The user password.</param>
        /// <param name="isPersistent">Whether to persist the sign-in across browser sessions.</param>
        /// <param name="lockoutOnFailure">Whether to lock the account on failure.</param>
        /// <returns>The result of the sign-in operation.</returns>
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
