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
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Web.Controllers;
/// <summary>
/// Controller get different information about flights tikets
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightSearchService _flightSearchService;
    private readonly ISuggestAmadeusService _suggestListService;

    /// <summary>
    /// we use DI for this
    /// </summary>
    public FlightsController(IFlightSearchService flightSearchService, ISuggestAmadeusService suggestListService)
    {
        _flightSearchService = flightSearchService;
        _suggestListService = suggestListService;

    } /// <summary>
      /// when user click button search he get diffetent tickets
      /// </summary>

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
    /// <summary>
    /// it is for text
    /// </summary>
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
   
}