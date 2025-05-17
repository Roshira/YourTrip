using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using YourTrips.Application.Amadeus.Models.Flight;
using YourTrips.Application.Interfaces;
using YourTrips.Application.Interfaces.Amadeus;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Core.DTOs.Amadeus;
using YourTrips.Core.DTOs.Amadeus.Flight;

namespace YourTrips.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightSearchService _flightSearchService;
    private readonly ISuggestListService _suggestListService;

    public FlightsController(IFlightSearchService flightSearchService, ISuggestListService suggestListService)
    {
        _flightSearchService = flightSearchService;
        _suggestListService = suggestListService;
    }

    [HttpPost("search")]
    [AllowAnonymous]
    public async Task<ActionResult<FlightSearchResponse>> SearchFlights(
       [FromBody] FlightSearchRequestDto request
        )
    {
        Console.WriteLine($"Що отримуємо:request");
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
}