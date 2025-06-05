using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Achievenent;

namespace YourTrips.Core.Interfaces.Achievements
{
    /// <summary>
    /// Interface for managing user achievements including checking, granting, deleting, and retrieving achievements.
    /// </summary>
    public interface ICreateAndShowAdchivement
    {
        /// <summary>
        /// Checks and updates achievements for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        Task CheckAchievementAsync(Guid userId);

        /// <summary>
        /// Retrieves a list of achievements earned by the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A list of achievement data transfer objects.</returns>
        Task<List<AchievementDto>> GetUserAchievementsAsync(Guid userId);

        /// <summary>
        /// Checks trip-related achievements for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        Task CheckTripsAsync(Guid userId);

        /// <summary>
        /// Grants an achievement to the user if it does not already exist.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="achievementId">The ID of the achievement to grant.</param>
        Task GrantAchievementIfNotExists(Guid userId, int achievementId);

        /// <summary>
        /// Deletes the specified achievement from the user’s achievements.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="achievementId">The ID of the achievement to delete.</param>
        Task DeleteAchievement(Guid userId, int achievementId);
    }
}
