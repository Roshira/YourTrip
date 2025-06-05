using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.Interfaces.Admin;

namespace YourTrips.Web.Controllers.Admin
{
    /// <summary>
    /// Controller for admin-specific operations.
    /// Requires user to be authenticated and in the "Admin" role.
    /// </summary>
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IUserSortingService _userSortingService;
        private readonly IParisRestaurants _parisRestaurants;
        private readonly IRestaurantSorter _restaurantSorter;

        /// <summary>
        /// Constructor for AdminController.
        /// </summary>
        /// <param name="userSortingService">Service to compare user sorting methods.</param>
        /// <param name="parisRestaurants">Service to get Paris restaurants data.</param>
        /// <param name="restaurantSorter">Service to sort restaurants.</param>
        public AdminController(IUserSortingService userSortingService, IParisRestaurants parisRestaurants, IRestaurantSorter restaurantSorter)
        {
            _userSortingService = userSortingService;
            _parisRestaurants = parisRestaurants;
            _restaurantSorter = restaurantSorter;
        }

        /// <summary>
        /// Compares different sorting methods on completed user routes.
        /// </summary>
        /// <returns>Result of sorting comparison.</returns>
        [HttpGet("sortCompletedRoutes")]
        public async Task<IActionResult> CompletedRoutes()
        {
            var result = await _userSortingService.CompareSortingMethods();
            return Ok(result);
        }

        /// <summary>
        /// Gets the list of restaurants in Paris.
        /// </summary>
        /// <returns>List of Paris restaurants.</returns>
        [HttpGet("parisRestaurants")]
        public async Task<IActionResult> GetParisRestaurants()
        {
            var restaurants = await _parisRestaurants.GetParisRestaurantsAsync();
            return Ok(restaurants);
        }

        /// <summary>
        /// Returns restaurants sorted sequentially.
        /// </summary>
        /// <returns>List of restaurants sorted sequentially.</returns>
        [HttpGet("sorted/sequential")]
        public async Task<IActionResult> GetSortedSequentially()
        {
            var sorted = await _restaurantSorter.SortSequentiallyAsync(null);
            return Ok(sorted);
        }

        /// <summary>
        /// Returns restaurants sorted in parallel.
        /// </summary>
        /// <returns>List of restaurants sorted in parallel.</returns>
        [HttpGet("sorted/parallel")]
        public async Task<IActionResult> GetSortedInParallel()
        {
            var sorted = await _restaurantSorter.SortInParallelAsync(null);
            return Ok(sorted);
        }

        /// <summary>
        /// Compares performance of sequential and parallel sorting methods.
        /// </summary>
        /// <returns>Comparison result of sorting methods.</returns>
        [HttpGet("sorting/compare")]
        public async Task<IActionResult> CompareSortingMethods()
        {
            var result = await _restaurantSorter.CompareSortingMethodsAsync();
            return Ok(result);
        }
    }
}
