using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Entities.Saved;


namespace YourTrips.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string IconUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string SavedYourMemoirs { get; set; }

        public ICollection<SavedHotel> SavedHotels { get; set; }
        public ICollection<SavedFlights> SavedFlights { get; set; }
        public ICollection<SavedBlaBlaCarTrips> SavedBlaBlaCarTrips { get; set; }
        public ICollection<SavedPlaces> SavedPlaces { get; set; }
        public ICollection<SavedTrainTrips> SavedTrainTrips { get; set; }

        public ICollection<UserAchievement> UserAchievements { get; set; }

        public ICollection<TripHistory> TripHistories { get; set; }
    }
}
