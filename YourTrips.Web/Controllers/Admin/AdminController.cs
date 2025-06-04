using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourTrips.Core.Interfaces.Admin;

namespace YourTrips.Web.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IUserSortingService _userSortingService;
        private readonly IParisRestaurants _parisRestaurants;
        private readonly IRestaurantSorter _restaurantSorter;
        public AdminController(IUserSortingService userSortingService, IParisRestaurants parisRestaurants, IRestaurantSorter restaurantSorter)
        {
            _userSortingService = userSortingService;
            _parisRestaurants = parisRestaurants;
            _restaurantSorter = restaurantSorter;
        }
        [HttpGet("sortCompletedRoutes")]
        public async Task<IActionResult> CompletedRoutes()
        {
            var result = await _userSortingService.CompareSortingMethods();

            return Ok(result);
        }
        [HttpGet("parisRestaurants")]
        public async Task<IActionResult> GetParisRestaurants()
        {
            var restaurants = await _parisRestaurants.GetParisRestaurantsAsync();
            return Ok(restaurants);
        }
        [HttpGet("sorted/sequential")]
        public async Task<IActionResult> GetSortedSequentially()
        {
            var sorted = await _restaurantSorter.SortSequentiallyAsync(null);
            return Ok(sorted);
        }

        [HttpGet("sorted/parallel")]
        public async Task<IActionResult> GetSortedInParallel()
        {
            var sorted = await _restaurantSorter.SortInParallelAsync(null);
            return Ok(sorted);
        }

        [HttpGet("sorting/compare")]
        public async Task<IActionResult> CompareSortingMethods()
        {
            var result = await _restaurantSorter.CompareSortingMethodsAsync();
            return Ok(result);
        }
    }

}
