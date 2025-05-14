using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using YourTrips.Application.Amadeus.Interfaces;
using YourTrips.Application.Amadeus.Models.Flight;

namespace YourTrips.Infrastructure.Services;

public class AmadeusFlightSearchService : IFlightSearchService
{
    private readonly HttpClient _httpClient;
    private readonly IAmadeusAuthService _authService;
    private readonly IAmadeusLocationService _locationService;

    public AmadeusFlightSearchService(
        HttpClient httpClient,
        IAmadeusAuthService authService,
        IAmadeusLocationService locationService)
    {
        _httpClient = httpClient;
        _authService = authService;
        _locationService = locationService;
    }

    public async Task<FlightSearchResponse> SearchFlightsAsync(
        string origin,
        string destination,
        DateTime departureDate,
        int passengers)
    {
        // Отримати токен доступу
        var token = await _authService.GetAccessTokenAsync();

        // Отримуємо IATA коди для міст
        var originCode = await _locationService.GetIataCodeFromCityNameAsync(origin);
        var destinationCode = await _locationService.GetIataCodeFromCityNameAsync(destination);

        // Формуємо запит
        var query = $"/v2/shopping/flight-offers" +
                    $"?originLocationCode={originCode}" +
                    $"&destinationLocationCode={destinationCode}" +
                    $"&departureDate={departureDate:yyyy-MM-dd}" +
                    $"&adults={passengers}";

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(query, UriKind.Relative)
        };

        // Додаємо токен до заголовку
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Відправити запит
        var response = await _httpClient.SendAsync(request);

        // Якщо помилка, викидаємо виняток з деталями
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Amadeus API returned {response.StatusCode}: {content}");
        }

        // Парсимо відповідь
        var result = await response.Content.ReadFromJsonAsync<FlightSearchResponse>();

        // Перетворюємо IATA коди в назви міст для кожного рейсу
      
        result.DepartureName = await _locationService.GetCityNameFromIataCodeAsync(originCode);
        result.ArrivalName = await _locationService.GetCityNameFromIataCodeAsync(destinationCode);


        return result!;
    }
}
