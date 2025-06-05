using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using YourTrips.Application.Interfaces.GoogleMaps;

/// <summary>
/// Controller for interacting with Google Places API to provide location-based data such as autocomplete suggestions,
/// detailed place information, and place search functionality.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _googleApiKey;
    private readonly IGooglePlacesService _placesService;

    /// <summary>
    /// Constructor for the <see cref="PlacesController"/>.
    /// </summary>
    /// <param name="httpClient">Injected HTTP client used for external API requests.</param>
    /// <param name="configuration">App configuration to get API key from settings.</param>
    /// <param name="placesService">Service that handles business logic for Google Places data retrieval.</param>
    public PlacesController(HttpClient httpClient, IConfiguration configuration, IGooglePlacesService placesService)
    {
        _httpClient = httpClient;
        _googleApiKey = configuration["GoogleMaps:ApiKey"];
        _placesService = placesService;
    }

    /// <summary>
    /// Returns autocomplete suggestions for a place name based on partial user input.
    /// </summary>
    /// <param name="input">The partial string input from the user.</param>
    /// <returns>JSON response from Google Places API with autocomplete predictions.</returns>
    /// <response code="200">Returns autocomplete suggestions.</response>
    /// <response code="400">If the input is null or empty.</response>
    [HttpGet("Autocomplete")]
    public async Task<IActionResult> GetAutocomplete([FromQuery] string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return BadRequest("Input is required.");

        var url = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={input}&key={_googleApiKey}&language=en";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return Content(content, "application/json");
    }

    /// <summary>
    /// Gets full details of a place using its place ID.
    /// </summary>
    /// <param name="placeId">The unique identifier of the place provided by Google Places.</param>
    /// <returns>Detailed information about the place.</returns>
    /// <response code="200">Returns the place details.</response>
    /// <response code="400">If the placeId is null or empty.</response>
    [HttpGet("details")]
    public async Task<IActionResult> GetDetails([FromQuery] string placeId)
    {
        if (string.IsNullOrWhiteSpace(placeId))
            return BadRequest("placeId is required.");

        var details = await _placesService.GetFullPlaceDetailsAsync(placeId);
        return Ok(details);
    }

    /// <summary>
    /// Searches for places based on a text query.
    /// </summary>
    /// <param name="query">The name or description of the place to search.</param>
    /// <returns>A list of place search results.</returns>
    /// <response code="200">Returns matching place results.</response>
    /// <response code="404">If no results found or API failed.</response>
    [HttpGet("search")]
    public async Task<IActionResult> SearchPlace([FromQuery] string query)
    {
        var results = await _placesService.GetPlacesInfoAsync(query);

        if (results == null)
            return NotFound(new { Message = "Place not found or API error." });

        return Ok(results);
    }
}
