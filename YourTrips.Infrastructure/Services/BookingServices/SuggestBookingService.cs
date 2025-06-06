using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;
using YourTrips.Application.DTOs.RapidBooking;
using YourTrips.Application.Interfaces.RapidBooking;

/// <summary>
/// Service for retrieving hotel and location suggestions from the Booking.com RapidAPI
/// </summary>
public class SuggestBookingService : ISuggestBookingService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the SuggestBookingService
    /// </summary>
    /// <param name="httpClient">HttpClient for making API requests</param>
    /// <param name="configuration">Configuration containing API settings</param>
    public SuggestBookingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        // Configure the HttpClient for Booking.com RapidAPI
        _httpClient.BaseAddress = new Uri("https://booking-com.p.rapidapi.com/v1/");
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["RapidBooking:ApiKey"]);
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "booking-com.p.rapidapi.com");
    }

    /// <summary>
    /// Retrieves hotel and city suggestions based on search term
    /// </summary>
    /// <param name="searchTerm">The term to search for (minimum 2 characters)</param>
    /// <returns>
    /// A collection of HotelSuggestionDto objects containing matching hotels and cities,
    /// or empty collection if search term is invalid
    /// </returns>
    public async Task<IEnumerable<HotelSuggestionDto>> GetSuggestionsAsync(string searchTerm)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
            return Enumerable.Empty<HotelSuggestionDto>();

        // Prepare API request
        var requestUrl = $"hotels/locations?locale=en-gb&name={Uri.EscapeDataString(searchTerm)}";
        var response = await _httpClient.GetAsync(requestUrl);

        // Ensure successful response
        response.EnsureSuccessStatusCode();

        // Parse JSON response
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        var results = new List<HotelSuggestionDto>();

        // Process each suggestion in the response
        foreach (var element in doc.RootElement.EnumerateArray())
        {
            if (element.TryGetProperty("dest_type", out var destTypeProp))
            {
                var destType = destTypeProp.GetString();

                // We're only interested in hotels and cities
                if (destType == "hotel" || destType == "city")
                {
                    var country = element.GetProperty("country").GetString();
                    var city = element.GetProperty("city_name").GetString();
                    string? hotelName = null;

                    // Only hotels have a name property
                    if (destType == "hotel")
                    {
                        hotelName = element.GetProperty("name").GetString();
                    }

                    results.Add(new HotelSuggestionDto
                    {
                        Country = country,
                        City = city,
                        HotelName = hotelName,
                        DestType = destType
                    });
                }
            }
        }

        return results;
    }
}