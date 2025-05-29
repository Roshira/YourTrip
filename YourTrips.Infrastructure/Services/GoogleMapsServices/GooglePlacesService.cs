using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces.GoogleMaps;
using YourTrips.Core.DTOs.GoogleMaps;
using YourTrips.Core.DTOs.GoogleMaps.PlaceDetail;

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
            Console.WriteLine($"JSON RESPONCE: {response}");
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"JSON PLACES: {json}");
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
