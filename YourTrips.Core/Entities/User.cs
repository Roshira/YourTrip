using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Entities.Saved;

namespace YourTrips.Core.Entities
{
    /// <summary>
    /// Represents an application user extending the ASP.NET IdentityUser with a GUID key.
    /// Contains additional properties related to user data and navigation properties for related entities.
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// The UTC date and time when the user was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Optional user memo or saved notes, limited to 500 characters.
        /// </summary>
        [StringLength(500)]
        public string? SavedYourMemoirs { get; set; }

        /// <summary>
        /// Collection of routes created by the user.
        /// </summary>
        public ICollection<Route> Routes { get; set; }

        /// <summary>
        /// Collection of achievements earned by the user.
        /// </summary>
        public ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
