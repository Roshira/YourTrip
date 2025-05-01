using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.Entities.Achievement
{
    public class UserAchievement
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }

        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
    }
}
