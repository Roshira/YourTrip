using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces.GoogleMaps;

namespace YourTrips.Infrastructure.Services.GoogleMapsServices
{
    public class GooglePlacesService : IGooglePlacesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GooglePlacesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"]; // в appsettings.json
        }

        public async Task<string> GetPlaceDetailsAsync(string placeId)
        {
            var url = $"https://maps.googleapis.com/maps/api/place/details/json" +
                      $"?place_id={placeId}" +
                      $"&key={_apiKey}" +
                      $"&language=en" +
                      $"&fields=name,formatted_address,photos,geometry,formatted_phone_number,website,opening_hours,rating,user_ratings_total,reviews,types,price_level";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode(); // кине помилку, якщо статус не 200

            return await response.Content.ReadAsStringAsync(); // можна десеріалізувати при потребі
        }
    }

}
