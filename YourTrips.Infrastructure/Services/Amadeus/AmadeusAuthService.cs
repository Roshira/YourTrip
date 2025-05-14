using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using YourTrips.Application.Amadeus.Interfaces;
using YourTrips.Application.Amadeus.Models;
using YourTrips.Application.Interfaces;

namespace YourTrips.Infrastructure.Services;

public class AmadeusAuthService : IAmadeusAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private string _accessToken;
    private DateTime _tokenExpiry;

    public AmadeusAuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Amadeus:ClientId"];
        _clientSecret = configuration["Amadeus:ClientSecret"];
        _httpClient.BaseAddress = new Uri(configuration["Amadeus:BaseUrl"]);
    }

    public async Task<string> GetAccessTokenAsync()
    {
        if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiry)
        {
            return _accessToken;
        }

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

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        _accessToken = authResponse.AccessToken;
        _tokenExpiry = DateTime.UtcNow.AddSeconds(authResponse.ExpiresIn - 60); // Запас 60 секунд
        Console.WriteLine("AccessToken: " + _accessToken);
        return _accessToken;
    }
}