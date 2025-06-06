using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using YourTrips.Application.Amadeus.Models.Flight;
using YourTrips.Application.DTOs.Amadeus.Flight;
using YourTrips.Application.Interfaces.Interfaces;

namespace YourTrips.Infrastructure.Services;

/// <summary>
/// Provides functionality for searching flight offers using the Amadeus API.
/// </summary>
public class AmadeusFlightSearchService : IFlightSearchService
{
    private readonly HttpClient _httpClient;
    private readonly IAmadeusAuthService _authService;
    private readonly IAmadeusLocationService _locationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AmadeusFlightSearchService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used for making requests to the Amadeus API.</param>
    /// <param name="authService">The authentication service used to obtain access tokens.</param>
    /// <param name="locationService">The location service used to convert between city names and IATA codes.</param>
    public AmadeusFlightSearchService(
        HttpClient httpClient,
        IAmadeusAuthService authService,
        IAmadeusLocationService locationService)
    {
        _httpClient = httpClient;
        _authService = authService;
        _locationService = locationService;
    }

    /// <summary>
    /// Searches for flight offers between the specified origin and destination cities using the Amadeus API.
    /// </summary>
    /// <param name="origin">The name of the origin city.</param>
    /// <param name="destination">The name of the destination city.</param>
    /// <param name="departureDate">The date of departure.</param>
    /// <param name="travelers">A list of travelers including their types (ADULT, CHILD, INFANT).</param>
    /// <param name="cabin">The preferred travel class (e.g., ECONOMY, BUSINESS).</param>
    /// <returns>A <see cref="FlightSearchResponse"/> containing the list of available flight offers.</returns>
    /// <exception cref="HttpRequestException">
    /// Thrown when the Amadeus API returns a non-success response or an error occurs while making the request.
    /// </exception>
    public async Task<FlightSearchResponse> SearchFlightsAsync(
        string origin,
        string destination,
        DateTime departureDate,
        List<TravelerDto> travelers,
        string cabin
    )
    {
        // Get the access token
        var token = await _authService.GetAccessTokenAsync();

        // Convert city names to IATA codes
        var originCode = await _locationService.GetIataCodeFromCityNameAsync(origin);
        var destinationCode = await _locationService.GetIataCodeFromCityNameAsync(destination);

        // Count traveler types
        int adults = travelers.Count(t => t.TravelerType == "ADULT");
        int children = travelers.Count(t => t.TravelerType == "CHILD");
        int infants = travelers.Count(t => t.TravelerType == "INFANT");

        // Build the query
        var query = $"/v2/shopping/flight-offers" +
                    $"?originLocationCode={originCode}" +
                    $"&destinationLocationCode={destinationCode}" +
                    $"&departureDate={departureDate:yyyy-MM-dd}" +
                    $"&adults={adults}" +
                    $"&children={children}" +
                    $"&infants={infants}" +
                    $"&travelClass={cabin.ToUpper()}";

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(query, UriKind.Relative)
        };

        // Add authorization header
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Send the request
        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Amadeus API returned {response.StatusCode}: {content}");
        }

        // Parse the response
        var result = await response.Content.ReadFromJsonAsync<FlightSearchResponse>();

        // Enrich response with readable city names
        result!.DepartureName = await _locationService.GetCityNameFromIataCodeAsync(originCode);
        result.ArrivalName = await _locationService.GetCityNameFromIataCodeAsync(destinationCode);

        return result;
    }
}
