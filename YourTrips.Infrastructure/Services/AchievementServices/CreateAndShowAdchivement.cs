using Microsoft.EntityFrameworkCore;
using YourTrips.Core.DTOs.Achievenent;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Interfaces.Achievements;
using YourTrips.Infrastructure.Data;

/// <summary>
/// Service to manage user achievements: retrieve, check, grant, and delete achievements based on user activity.
/// </summary>
public class CreateAndShowAdchivement : ICreateAndShowAdchivement
{
    private readonly YourTripsDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAndShowAdchivement"/> class.
    /// </summary>
    /// <param name="context">Database context for accessing achievement and user data.</param>
    public CreateAndShowAdchivement(YourTripsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all achievements earned by the specified user, ordered by the date they were earned.
    /// </summary>
    /// <param name="userId">The ID of the user whose achievements are being retrieved.</param>
    /// <returns>A list of <see cref="AchievementDto"/> representing the user's achievements.</returns>
    public async Task<List<AchievementDto>> GetUserAchievementsAsync(Guid userId)
    {
        return await _context.UserAchievements
            .Where(ua => ua.UserId == userId)
            .OrderBy(ua => ua.EarnedAt)
            .Select(ua => new AchievementDto
            {
                Id = ua.Achievement.Id,
                Title = ua.Achievement.Name,
                Description = ua.Achievement.Description,
                IconUrl = ua.Achievement.IconUrl,
                EarnedAt = ua.EarnedAt
            }).ToListAsync();
    }

    /// <summary>
    /// Checks and updates achievements for the specified user.
    /// Currently this method checks achievements related to completed trips.
    /// </summary>
    /// <param name="userId">The ID of the user to check achievements for.</param>
    public async Task CheckAchievementAsync(Guid userId)
    {
        await CheckTripsAsync(userId);
    }

    /// <summary>
    /// Checks the number of completed trips for the user and grants or deletes achievements accordingly.
    /// </summary>
    /// <param name="userId">The ID of the user to check trips for.</param>
    public async Task CheckTripsAsync(Guid userId)
    {
        var completedTrips = await _context.Routes
            .Where(r => r.UserId == userId && r.IsCompleted)
            .CountAsync();

        // Mapping between minimum number of completed trips and achievement IDs
        var achievementIdsByCompletedTrips = new Dictionary<int, int>
        {
            { 1, 1 },
            { 3, 2 },
            { 5, 3 },
            { 10, 4 }
        };

        foreach (var pair in achievementIdsByCompletedTrips)
        {
            if (completedTrips >= pair.Key)
            {
                await GrantAchievementIfNotExists(userId, pair.Value);
            }
            else
            {
                await DeleteAchievement(userId, pair.Value);
            }
        }
    }

    /// <summary>
    /// Grants an achievement to the user if it has not already been granted.
    /// </summary>
    /// <param name="userId">The ID of the user to grant the achievement to.</param>
    /// <param name="achievementId">The ID of the achievement to grant.</param>
    public async Task GrantAchievementIfNotExists(Guid userId, int achievementId)
    {
        var alreadyHas = await _context.UserAchievements
            .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);

        if (!alreadyHas)
        {
            _context.UserAchievements.Add(new UserAchievement
            {
                UserId = userId,
                AchievementId = achievementId,
                EarnedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Deletes a specified achievement from the user's achievements, if it exists.
    /// </summary>
    /// <param name="userId">The ID of the user to remove the achievement from.</param>
    /// <param name="achievementId">The ID of the achievement to remove.</param>
    public async Task DeleteAchievement(Guid userId, int achievementId)
    {
        var userAchievement = await _context.UserAchievements
            .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);

        if (userAchievement != null)
        {
            _context.UserAchievements.Remove(userAchievement);
            await _context.SaveChangesAsync();
        }
    }
}
