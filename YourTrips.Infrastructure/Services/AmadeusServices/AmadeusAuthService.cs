using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using YourTrips.Application.Amadeus.Models;
using YourTrips.Application.Interfaces;
using YourTrips.Application.Interfaces.Interfaces;

namespace YourTrips.Infrastructure.Services;

/// <summary>
/// Provides functionality for authenticating with the Amadeus API and retrieving access tokens.
/// </summary>
public class AmadeusAuthService : IAmadeusAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private string _accessToken;
    private DateTime _tokenExpiry;

    /// <summary>
    /// Initializes a new instance of the <see cref="AmadeusAuthService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used to communicate with the Amadeus authentication server.</param>
    /// <param name="configuration">The configuration containing Amadeus API credentials and base URL.</param>
    public AmadeusAuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Amadeus:ClientId"];
        _clientSecret = configuration["Amadeus:ClientSecret"];
        _httpClient.BaseAddress = new Uri(configuration["Amadeus:BaseUrl"]);
    }

    /// <summary>
    /// Retrieves a valid access token for the Amadeus API, using caching to avoid unnecessary token requests.
    /// </summary>
    /// <returns>A valid OAuth 2.0 access token string.</returns>
    /// <exception cref="HttpRequestException">Thrown if the token request fails or the response is not successful.</exception>
    public async Task<string> GetAccessTokenAsync()
    {
        // Return cached token if it's still valid
        if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiry)
        {
            return _accessToken;
        }

        // Build the authentication request
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/v1/security/oauth2/token", UriKind.Relative),
            Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret)
            })
        };

        // Send the request and ensure success
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        // Parse the response
        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        _accessToken = authResponse.AccessToken;
        _tokenExpiry = DateTime.UtcNow.AddSeconds(authResponse.ExpiresIn - 60); // 60-second buffer before expiry

        Console.WriteLine("AccessToken: " + _accessToken);

        return _accessToken;
    }
}
