using Microsoft.EntityFrameworkCore;
using YourTrips.Core.DTOs.Achievenent;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Interfaces.Achievements;
using YourTrips.Infrastructure.Data;

public class CreateAndShowAdchivement : ICreateAndShowAdchivement
{
    private readonly YourTripsDbContext _context;

    public CreateAndShowAdchivement(YourTripsDbContext context)
    {
        _context = context;
    }

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

    public async Task CheckAchievementAsync(Guid userId)
    {
        await CheckTripsAsync(userId);
    }

    public async Task CheckTripsAsync(Guid userId)
    {
        var completedTrips = await _context.Routes
            .Where(r => r.UserId == userId && r.IsCompleted)
            .CountAsync();

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
