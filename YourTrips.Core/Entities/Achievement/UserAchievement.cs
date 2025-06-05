using System;

namespace YourTrips.Core.Entities.Achievement
{
    /// <summary>
    /// Represents the relationship between a user and an achievement they have earned.
    /// </summary>
    public class UserAchievement
    {
        /// <summary>
        /// Primary key of the user-achievement record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the user who earned the achievement.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation property to the user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Foreign key to the achievement earned.
        /// </summary>
        public int AchievementId { get; set; }

        /// <summary>
        /// Navigation property to the achievement.
        /// </summary>
        public Achievement Achievement { get; set; }

        /// <summary>
        /// The date and time when the achievement was earned.
        /// </summary>
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
    }
}
