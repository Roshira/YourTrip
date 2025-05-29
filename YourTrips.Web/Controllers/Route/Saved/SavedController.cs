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
        private readonly ISaveDelId _saveDelId;

        public SavedController(UserManager<User> userManager, ISavDelJSONModel savDelJSONModel, ISaveDelId saveDelId)
        {
            _saveDelId = saveDelId;
            _userManager = userManager;
            _savDelJSONModel = savDelJSONModel;
        }

        [HttpPost("flights")]
        [Authorize]
        public async Task<IActionResult> SavedFlights([FromQuery] string flightsJson, int routeId)
        {
            await _savDelJSONModel.SaveJsonAsync<SavedFlights>(flightsJson, routeId);
            return Ok();
        }

        [HttpPost("hotel")]
        [Authorize]
        public async Task<IActionResult> SavedHotel([FromQuery] string hotelJson, int routeId)
        {
            await _savDelJSONModel.SaveJsonAsync<SavedHotel>(hotelJson, routeId);
            return Ok();
        }
        [HttpPost("places")]
        [Authorize]
        public async Task<IActionResult> SavedPlaces([FromQuery] string placeId, int routeId)
        {
            await _saveDelId.SaveIdAsync<SavedPlaces>(placeId, routeId);
            return Ok();
        }
    }
}
