using System.Net;
using System.Net.Http.Json;
using System.Web;
using YourTrips.Application.Amadeus.Interfaces;
using YourTrips.Application.Amadeus.Models;
using YourTrips.Core.DTOs.Amadeus;

namespace YourTrips.Infrastructure.Services;

public class AmadeusLocationService : IAmadeusLocationService
{
    private readonly HttpClient _httpClient;
    private readonly IAmadeusAuthService _authService;

    public AmadeusLocationService(HttpClient httpClient, IAmadeusAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

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
