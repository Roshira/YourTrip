using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.GoogleMaps.PlaceDetail;
using YourTrips.Core.DTOs.GoogleMaps;
using Microsoft.Extensions.Configuration;
using YourTrips.Application.Interfaces.GoogleMaps;
using YourTrips.Core.DTOs.Admin.Data;
using YourTrips.Core.Interfaces.Admin;

namespace YourTrips.Infrastructure.Services.Admin.Data
{
    public class ParisRestaurants : IParisRestaurants
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;


        public ParisRestaurants(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"];

        }
        public async Task<List<RestaurantDto>> GetParisRestaurantsAsync()
        {
            const string locationName = "restaurants in Paris";
            var allResults = new List<RestaurantDto>();
            string nextPageToken = null;
            int maxRequests = 5; // Обмеження кількості запитів через API limits

            for (int i = 0; i < maxRequests; i++)
            {
                string url = $"https://maps.googleapis.com/maps/api/place/textsearch/json?" +
                            $"query={Uri.EscapeDataString(locationName)}&" +
                $"key={_apiKey}" +
                            (nextPageToken != null ? $"&pagetoken={nextPageToken}" : "");

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    // Можна додати логування помилки
                    break;
                }

                var json = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(json);

                // Обробка кожної знахідки
                foreach (var item in result.results)
                {
                    // Отримання фото (якщо є)
                    var photoUrls = new List<string>();
                    if (item.photos != null)
                    {
                        foreach (var photo in item.photos)
                        {
                            string photoReference = photo.photo_reference;
                            string photoUrl = $"https://maps.googleapis.com/maps/api/place/photo?" +
                                            $"maxwidth=400&photo_reference={photoReference}&key={_apiKey}";
                            photoUrls.Add(photoUrl);
                        }
                    }

                    // Отримання додаткової інформації про ресторан
                    var restaurantDetails = await GetPlaceDetailsAsync(item.place_id.ToString());

                    allResults.Add(new RestaurantDto
                    {
                        Id = item.place_id,
                        Name = item.name,
                        Address = item.formatted_address,
                        Rating = item.rating ?? 0,
                        PriceLevel = item.price_level ?? 0,
                        PhotoUrls = photoUrls,
                        Types = item.types?.ToObject<List<string>>() ?? new List<string>(),
                        IsOpenNow = restaurantDetails?.OpeningHours?.OpenNow ?? false,
                        PhoneNumber = restaurantDetails?.PhoneNumber,
                        Website = restaurantDetails?.Website
                    });
                }

                // Перевірка наявності наступної сторінки
                nextPageToken = result.next_page_token?.ToString();
                if (string.IsNullOrEmpty(nextPageToken))
                    break;

                // Google API вимагає затримки між запитами для next_page_token
                await Task.Delay(2000);
            }

            return allResults;
        }

        private async Task<RestaurantDetailsDto> GetPlaceDetailsAsync(string placeId)
        {
            string url = $"https://maps.googleapis.com/maps/api/place/details/json?" +
                        $"place_id={placeId}&" +
                        $"fields=opening_hours,formatted_phone_number,website&" +
                        $"key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(json);

            return new RestaurantDetailsDto
            {
                OpeningHours = new OpeningHoursDto
                {
                    OpenNow = result.result.opening_hours?.open_now ?? false
                },
                PhoneNumber = result.result.formatted_phone_number,
                Website = result.result.website
            };
        }
    }
}
