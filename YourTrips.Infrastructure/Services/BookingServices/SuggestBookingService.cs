using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;
using YourTrips.Application.Interfaces.RapidBooking;
using YourTrips.Core.DTOs.RapidBooking;

public class SuggestBookingService : ISuggestBookingService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public SuggestBookingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        _httpClient.BaseAddress = new Uri("https://booking-com.p.rapidapi.com/v1/");
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["RapidBooking:ApiKey"]);
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "booking-com.p.rapidapi.com");
    }

    public async Task<IEnumerable<HotelSuggestionDto>> GetSuggestionsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
            return Enumerable.Empty<HotelSuggestionDto>();

        var requestUrl = $"hotels/locations?locale=en-gb&name={Uri.EscapeDataString(searchTerm)}";
        var response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        var results = new List<HotelSuggestionDto>();

        foreach (var element in doc.RootElement.EnumerateArray())
        {
            if (element.TryGetProperty("dest_type", out var destTypeProp))
            {
                var destType = destTypeProp.GetString();

                if (destType == "hotel" || destType == "city")
                {
                    var country = element.GetProperty("country").GetString();
                    var city = element.GetProperty("city_name").GetString();
                    string? hotelName = null;
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
