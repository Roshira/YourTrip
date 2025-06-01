using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Web.Controllers.Route
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService review)
        {
            _reviewService = review;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ReviewDto review)
        {
            var result = await _reviewService.LeaveReviewAsync(review);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }


    }
}
