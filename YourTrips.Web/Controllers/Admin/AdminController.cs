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

        public AdminController(IUserSortingService userSortingService)
        {
            _userSortingService = userSortingService;
        }
        [HttpGet("sortCompletedRoutes")]
        public async Task<IActionResult> CompletedRoutes()
        {
            var result = await _userSortingService.CompareSortingMethods();
        
            return Ok(result);
        }
    }
}
