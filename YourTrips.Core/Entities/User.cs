using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Entities.Saved;
using YourTrips.Core.Entities;


namespace YourTrips.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? SavedYourMemoirs { get; set; }

        public ICollection<Route> Routes { get; set; }
        public ICollection<UserAchievement> UserAchievements { get; set; }

    }
}
