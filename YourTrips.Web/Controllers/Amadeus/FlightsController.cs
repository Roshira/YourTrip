using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using YourTrips.Application.Amadeus.Models.Flight;
using YourTrips.Application.Interfaces;
using YourTrips.Application.Interfaces.Amadeus;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Core.DTOs.Amadeus;
using YourTrips.Core.DTOs.Amadeus.Flight;
using YourTrips.Core.Entities;
using YourTrips.Core.Entities.Saved;
using YourTrips.Core.Interfaces.SavedServices;

namespace YourTrips.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightSearchService _flightSearchService;
    private readonly ISuggestAmadeusService _suggestListService;
    private readonly UserManager<User> _userManager;
    private readonly ISavDelJSONModel _savDelJSONModel;

    public FlightsController(IFlightSearchService flightSearchService, ISuggestAmadeusService suggestListService, UserManager<User> userManager, ISavDelJSONModel savDelJSONModel)
    {
        _flightSearchService = flightSearchService;
        _suggestListService = suggestListService;
        _userManager = userManager;
        _savDelJSONModel = savDelJSONModel; 
    }

    [HttpPost("search")]
    [AllowAnonymous]
    public async Task<ActionResult<FlightSearchResponse>> SearchFlights(
       [FromBody] FlightSearchRequestDto request
        )
    {
        try
        {
            var result = await _flightSearchService.SearchFlightsAsync(
                request.Origin, request.Destination, request.Date, request.Travellers, request.Cabin);

            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error accessing Amadeus API: {ex.Message}");
        }
    }

    [HttpGet("SuggestList")]
    public async Task<IActionResult> SuggestList(string text)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("Search text cannot be empty.");

            // Отримуємо дані з Amadeus API
            var suggestions = await _suggestListService.GetLocationSuggestions(text);

            return Ok(suggestions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpPost("Saved")]
    [Authorize]
    public async Task<IActionResult> Saved([FromQuery] string flightsJson)
    {
        var user = await _userManager.GetUserAsync(User);
        await _savDelJSONModel.SaveJsonAsync<SavedFlights>(user.Id, flightsJson);
        return Ok();
    }
}