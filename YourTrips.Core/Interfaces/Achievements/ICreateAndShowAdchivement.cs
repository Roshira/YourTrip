using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Achievenent;

namespace YourTrips.Core.Interfaces.Achievements
{
    public interface ICreateAndShowAdchivement
    {
        Task CheckAchievementAsync(Guid userId);
        Task<List<AchievementDto>> GetUserAchievementsAsync(Guid userId);
        Task CheckTripsAsync(Guid userId);
        Task GrantAchievementIfNotExists(Guid userId, int achievementId);
        Task DeleteAchievement(Guid userId, int achievementId);
    }
}
