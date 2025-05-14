using Microsoft.AspNetCore.Mvc;
using YourTrips.Application.Amadeus.Interfaces;
using YourTrips.Application.Amadeus.Models.Flight;
using YourTrips.Application.Interfaces;

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

    [HttpGet("search")]
    public async Task<ActionResult<FlightSearchResponse>> SearchFlights(
        [FromQuery] string origin,
        [FromQuery] string destination,
        [FromQuery] DateTime departureDate,
        [FromQuery] int passengers = 1)
    {
        try
        {
            var result = await _flightSearchService.SearchFlightsAsync(
                origin, destination, departureDate, passengers);

            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error accessing Amadeus API: {ex.Message}");
        }
    }
}