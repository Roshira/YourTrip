using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using YourTrips.Application.Amadeus.Interfaces;
using YourTrips.Application.Amadeus.Models.Flight;
using YourTrips.Core.DTOs.Amadeus.Flight;

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
        List<TravelerDto> travelers,
        string cabin
        )
    {
        // Отримати токен доступу
        var token = await _authService.GetAccessTokenAsync();

        // Отримуємо IATA коди для міст
        var originCode = await _locationService.GetIataCodeFromCityNameAsync(origin);
        var destinationCode = await _locationService.GetIataCodeFromCityNameAsync(destination);
        int adults = travelers.Count(t => t.TravelerType == "ADULT");
        int children = travelers.Count(t => t.TravelerType == "CHILD");
        int infants = travelers.Count(t => t.TravelerType == "INFANT");
        // Формуємо запит
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
