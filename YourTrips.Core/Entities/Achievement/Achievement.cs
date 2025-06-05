using System;
using System.Collections.Generic;

namespace YourTrips.Core.Entities.Achievement
{
    /// <summary>
    /// Represents an achievement that users can earn in the system.
    /// </summary>
    public class Achievement
    {
        /// <summary>
        /// Primary key of the achievement.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name or title of the achievement.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description or details about the achievement.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL of the icon image representing the achievement.
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// Collection of user-achievements linking users to this achievement.
        /// </summary>
        public ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
