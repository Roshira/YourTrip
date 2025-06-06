using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YourTrips.Application.DTOs.GoogleMaps;
using YourTrips.Application.DTOs.GoogleMaps.PlaceDetail;
using YourTrips.Application.Interfaces.GoogleMaps;

namespace YourTrips.Infrastructure.Services.GoogleMapsServices
{
    /// <summary>
    /// Service for interacting with Google Places API.
    /// Provides methods for retrieving place search results and detailed place information.
    /// </summary>
    public class GooglePlacesService : IGooglePlacesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="GooglePlacesService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client instance used to send requests.</param>
        /// <param name="configuration">Application configuration for retrieving the API key.</param>
        public GooglePlacesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"]; // Stored in appsettings.json
        }

        /// <summary>
        /// Retrieves detailed information about a place by its Google Place ID.
        /// </summary>
        /// <param name="placeId">The unique identifier of the place.</param>
        /// <returns>A <see cref="PlaceDetailsDto"/> object with detailed information about the place.</returns>
        public async Task<PlaceDetailsDto> GetFullPlaceDetailsAsync(string placeId)
        {
            var url = $"https://maps.googleapis.com/maps/api/place/details/json" +
                      $"?place_id={placeId}" +
                      $"&key={_apiKey}" +
                      $"&language=en" +
                      $"&fields=name,formatted_address,photos,geometry,formatted_phone_number,international_phone_number," +
                      $"website,opening_hours,rating,user_ratings_total,reviews,types,url,vicinity," +
                      $"business_status,editorial_summary,price_level";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var details = JsonConvert.DeserializeObject<GooglePlaceDetailsResponse>(json);
            var r = details?.Result;

            return new PlaceDetailsDto
            {
                Name = r?.Name,
                Formatted_Address = r?.Formatted_Address,
                Vicinity = r?.Vicinity,
                Phone = r?.Formatted_Phone_Number,
                InternationalPhoneNumber = r?.International_Phone_Number,
                Website = r?.Website,
                Latitude = r?.Geometry?.Location?.Lat,
                Longitude = r?.Geometry?.Location?.Lng,
                PhotoUrl = r?.Photos?.Take(6).Select(p => $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={p.Photo_Reference}&key={_apiKey}").ToList(),
                Reviews = r?.Reviews?.Select(rv => new ReviewDto
                {
                    AuthorName = rv.Author_Name,
                    Rating = rv.Rating,
                    Text = rv.Text
                }).ToList(),
                Opening_Now = r?.Opening_Hours?.Open_Now,
                WeekdayText = r?.Opening_Hours?.Weekday_Text,
                Rating = r?.Rating,
                UserRatingsTotal = r?.User_Ratings_Total,
                Types = r?.Types,
                Price_Level = r?.Price_Level,
                Url = r?.Url,
                BusinessStatus = r?.Business_Status,
                Summary = r?.Editorial_Summary?.Overview
            };
        }

        /// <summary>
        /// Searches for places based on the provided location name.
        /// </summary>
        /// <param name="locationName">The name of the location to search for.</param>
        /// <returns>A list of <see cref="PlaceSearchResultDto"/> representing the search results.</returns>
        public async Task<List<PlaceSearchResultDto>> GetPlacesInfoAsync(string locationName)
        {
            var allResults = new List<PlaceSearchResultDto>();
            string url = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query={Uri.EscapeDataString(locationName)}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return allResults;

            var json = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(json);

            foreach (var item in result.results)
            {
                string photoUrl = null;

                if (item.photos != null && item.photos.Count > 0)
                {
                    string photoReference = item.photos[0].photo_reference;
                    photoUrl = $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference={photoReference}&key={_apiKey}";
                }

                allResults.Add(new PlaceSearchResultDto
                {
                    Id = item.place_id,
                    Name = item.name,
                    Address = item.formatted_address,
                    PhotoUrls = photoUrl
                });
            }

            return allResults;
        }
    }
}
