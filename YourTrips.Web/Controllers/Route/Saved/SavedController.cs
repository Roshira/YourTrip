using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Application.Interfaces.Amadeus;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Core.Entities;
using YourTrips.Core.Entities.Saved;
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Web.Controllers.Route.Saved
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavedController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ISavDelJSONModel _savDelJSONModel;

        public SavedController(UserManager<User> userManager, ISavDelJSONModel savDelJSONModel)
        {
           
            _userManager = userManager;
            _savDelJSONModel = savDelJSONModel;
        }

        [HttpPost("flights")]
        [Authorize]
        public async Task<IActionResult> SavedFlights([FromQuery] string flightsJson, int routeId)
        {
            var user = await _userManager.GetUserAsync(User);
            await _savDelJSONModel.SaveJsonAsync<SavedFlights>(flightsJson, routeId);
            return Ok();
        }

        [HttpPost("hotel")]
        [Authorize]
        public async Task<IActionResult> SavedHotel([FromQuery] string hotelJson, int routeId)
        {
            var user = await _userManager.GetUserAsync(User);
            await _savDelJSONModel.SaveJsonAsync<SavedHotel>(hotelJson, routeId);
            return Ok();
        }
    }
}
