using System.Net;
using System.Net.Http.Json;
using System.Web;
using YourTrips.Application.Amadeus.Models;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Core.DTOs.Amadeus.Locations;

namespace YourTrips.Infrastructure.Services;

/// <summary>
/// Service for interacting with Amadeus API to retrieve location information,
/// such as converting city names to IATA codes and vice versa.
/// </summary>
public class AmadeusLocationService : IAmadeusLocationService
{
    private readonly HttpClient _httpClient;
    private readonly IAmadeusAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AmadeusLocationService"/> class.
    /// </summary>
    /// <param name="httpClient">HttpClient instance used for making API requests.</param>
    /// <param name="authService">Authentication service for obtaining access tokens.</param>
    public AmadeusLocationService(HttpClient httpClient, IAmadeusAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    /// <summary>
    /// Asynchronously gets the IATA code for a given city name.
    /// </summary>
    /// <param name="cityName">The name of the city to find the IATA code for.</param>
    /// <returns>
    /// The IATA code as a string if found; otherwise, null.
    /// </returns>
    public async Task<string?> GetIataCodeFromCityNameAsync(string cityName)
    {
        string accessToken = await _authService.GetAccessTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var encoded = HttpUtility.UrlEncode(cityName);
        var response = await _httpClient.GetAsync(
            $"/v1/reference-data/locations?subType=CITY,AIRPORT&keyword={encoded}&page[limit]=1");

        if (!response.IsSuccessStatusCode)
            return null;

        Console.WriteLine("Getting IATA code for: " + cityName);

        var result = await response.Content.ReadFromJsonAsync<LocationResponse>();
        return result?.Data?.FirstOrDefault()?.IataCode;
    }

    /// <summary>
    /// Asynchronously gets the city name for a given IATA code.
    /// </summary>
    /// <param name="iataCode">The IATA code to find the corresponding city name for.</param>
    /// <returns>
    /// The city name as a string if found; otherwise, null.
    /// </returns>
    public async Task<string?> GetCityNameFromIataCodeAsync(string iataCode)
    {
        string accessToken = await _authService.GetAccessTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var encoded = HttpUtility.UrlEncode(iataCode);
        var response = await _httpClient.GetAsync(
            $"/v1/reference-data/locations?subType=CITY,AIRPORT&keyword={encoded}&page[limit]=1");

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<LocationResponse>();
        return result?.Data?.FirstOrDefault()?.Name;
    }
}
