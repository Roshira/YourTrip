using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Application.Interfaces.Amadeus;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Core.DTOs.Route.Saved;
using YourTrips.Core.Entities;
using YourTrips.Core.Entities.Saved;
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Web.Controllers.Route.Saved
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SavedController : Controller
    {
        private readonly ISavDelJSONModel _savDelJSONModel;

        public SavedController(ISavDelJSONModel savDelJSONModel)
        {
            _savDelJSONModel = savDelJSONModel;
        }

        [HttpPost("flights")]
        public async Task<IActionResult> SavedFlights([FromBody] SavedDto savedDto)
        {
            await _savDelJSONModel.SaveJsonAsync<SavedFlights>(savedDto);
            return Ok();
        }

        [HttpPost("hotel")]
        public async Task<IActionResult> SavedHotel([FromBody] SavedDto savedDto)
        {
            await _savDelJSONModel.SaveJsonAsync<SavedHotel>(savedDto);
            return Ok();
        }
        [HttpPost("places")]
     
        public async Task<IActionResult> SavedPlaces([FromBody] SavedDto savedDto)
        {
            await _savDelJSONModel.SaveJsonAsync<SavedPlaces>(savedDto);
            return Ok();
        }
        [HttpDelete("deleteFlighrs")]
        public async Task<IActionResult> DeleteFlights([FromBody] DeleteSavedDto deleteSavedDto)
        {
            await _savDelJSONModel.DeleteJsonAsync<SavedFlights>(deleteSavedDto);
            return Ok();
        }
        [HttpDelete("deleteHotels")]
        public async Task<IActionResult> DeleteHotel([FromBody] DeleteSavedDto deleteSavedDto)
        {
            await _savDelJSONModel.DeleteJsonAsync<SavedHotel>(deleteSavedDto);
            return Ok();
        }
        [HttpDelete("deletePlaces")]
        public async Task<IActionResult> DeletePlaces([FromBody] DeleteSavedDto deleteSavedDto)
        {
            await _savDelJSONModel.DeleteJsonAsync<SavedPlaces>(deleteSavedDto);
            return Ok();
        }

    }
}
