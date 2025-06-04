using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.Interfaces.Achievements;
using YourTrips.Web.Extensions;

namespace YourTrips.Web.Controllers.Achievements
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AchievementController : Controller
    {
        private readonly ICreateAndShowAdchivement _achievementService;

        public AchievementController(ICreateAndShowAdchivement achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet("show")]
        public async Task<IActionResult> GetUserAchievements()
        {
            var userId = User.GetUserId();
            var achievements = await _achievementService.GetUserAchievementsAsync(userId);
            return Ok(achievements);
        }
    }
}
