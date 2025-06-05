using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.Interfaces.Achievements;
using YourTrips.Web.Extensions;

namespace YourTrips.Web.Controllers.Achievements
{
    /// <summary>
    /// This controller work with achievements
    /// </summary>
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
        /// <summary>
        /// this show your chievements
        /// </summary>
        /// <returns>you get Dto with photoUrl, describe, name</returns>
        [HttpGet("show")]
        public async Task<IActionResult> GetUserAchievements()
        {
            var userId = User.GetUserId();
            var achievements = await _achievementService.GetUserAchievementsAsync(userId);
            return Ok(achievements);
        }
    }
}
