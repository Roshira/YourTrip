using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Interfaces.Achievements;
using YourTrips.Core.Interfaces.Routes.Saved;
using YourTrips.Web.Extensions;

namespace YourTrips.Web.Controllers.Route
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ICreateAndShowAdchivement _achievements;
        public ReviewController(IReviewService review, ICreateAndShowAdchivement achievements)
        {
            _achievements = achievements;
            _reviewService = review;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ReviewDto review)
        {
            var result = await _reviewService.LeaveReviewAsync(review);

            if (!result.IsSuccess)
                return BadRequest(result.Message);
            var userId = User.GetUserId();
            await _achievements.CheckAchievementAsync(userId);
            return Ok(result);
        }


    }
}
