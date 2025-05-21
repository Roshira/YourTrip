using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using YourTrips.Application.Interfaces.GoogleMaps;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _googleApiKey;
    private readonly IGooglePlacesService _placesService;

    public PlacesController(HttpClient httpClient, IConfiguration configuration, IGooglePlacesService placesService)
    {
        _httpClient = httpClient;
        _googleApiKey = configuration["GoogleMaps:ApiKey"];
        _placesService = placesService;
    }

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
    [HttpGet("details")]
    public async Task<IActionResult> GetDetails([FromQuery] string placeId)
    {
        if (string.IsNullOrEmpty(placeId))
            return BadRequest("placeId is required.");

        var json = await _placesService.GetPlaceDetailsAsync(placeId);
        return Content(json, "application/json"); // або можна десеріалізувати і повернути об'єкт
    }
}
