using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Application.Amadeus.Interfaces;
using YourTrips.Application.Amadeus.Models.Flight;
using YourTrips.Application.Interfaces;
using YourTrips.Core.DTOs.Amadeus;
using YourTrips.Core.DTOs.Amadeus.Flight;

namespace YourTrips.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightSearchService _flightSearchService;

    public FlightsController(IFlightSearchService flightSearchService)
    {
        _flightSearchService = flightSearchService;
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
}