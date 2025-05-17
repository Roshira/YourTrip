using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using global::YourTrips.Application.RapidBooking.Interfaces;
using global::YourTrips.Core.DTOs.RapidBooking;
using Microsoft.Extensions.Configuration;
using MimeKit.Cryptography;
using YourTrips.Core.DTOs.RapidBooking.AdditionalDTO;

namespace YourTrips.Infrastructure.RapidBooking.Services
{
    public class BookingApiService : IBookingApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiHost;

        public BookingApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["RapidBooking:ApiKey"];
            _apiHost = config["RapidBooking:BaseUrl"];
        }

        public async Task<IEnumerable<HotelResultDto>> SearchHotelsAsync(HotelSearchRequestDto request)
        {
          
            // 1. Отримати destination_id та dest_type
            var locationUrl = $"https://{_apiHost}/v1/hotels/locations?name={Uri.EscapeDataString(request.City)}&locale=en-us";
            var locationRequest = new HttpRequestMessage(HttpMethod.Get, locationUrl);
            AddHeaders(locationRequest);

            HttpResponseMessage locationResponse;
            try
            {
                locationResponse = await _httpClient.SendAsync(locationRequest);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Помилка HTTP запиту локації: {e.Message}");
                return Enumerable.Empty<HotelResultDto>();
            }

            Console.WriteLine($"Статус-код відповіді локації: {locationResponse.StatusCode}");
            var locationJson = await locationResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"JSON відповіді локації: {locationJson}");

            if (!locationResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Помилка API локації: {locationResponse.ReasonPhrase}");
                return Enumerable.Empty<HotelResultDto>();
            }

            List<BookingLocationDto> locationData;
            try
            {
                locationData = JsonSerializer.Deserialize<List<BookingLocationDto>>(locationJson);
                if (locationData == null || !locationData.Any())
                {
                    Console.WriteLine("Не знайдено жодної локації");
                    return Enumerable.Empty<HotelResultDto>();
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Помилка десеріалізації JSON локації: {ex.Message}");
                return Enumerable.Empty<HotelResultDto>();
            }

            var destination = locationData.FirstOrDefault(l => l.DestType == "city") ?? locationData.First();
            if (string.IsNullOrEmpty(destination.DestId))
            {
                Console.WriteLine("Не вдалося отримати DestId");
                return Enumerable.Empty<HotelResultDto>();
            }

            Console.WriteLine($"Використовуємо DestId: {destination.DestId}, DestType: {destination.DestType}");

            // 2. Пошук готелів
            var checkIn = request.CheckInDate.ToString("yyyy-MM-dd");
            var checkOut = request.CheckOutDate.ToString("yyyy-MM-dd");

            var hotelsUrl = new StringBuilder($"https://{_apiHost}/v1/hotels/search?");
            hotelsUrl.Append($"dest_id={destination.DestId}");
            hotelsUrl.Append($"&dest_type={destination.DestType}");
            hotelsUrl.Append($"&checkin_date={checkIn}");
            hotelsUrl.Append($"&checkout_date={checkOut}");
            hotelsUrl.Append($"&adults_number={request.Adults}");
            hotelsUrl.Append($"&room_number={request.Rooms}");
            hotelsUrl.Append($"&order_by=popularity");
            hotelsUrl.Append($"&locale=en-us");
            hotelsUrl.Append($"&units=metric");
            hotelsUrl.Append($"&filter_by_currency=USD");

            if (request.Children > 0)
            {
                hotelsUrl.Append($"&children_number={request.Children}");
            }

            var hotelsRequest = new HttpRequestMessage(HttpMethod.Get, hotelsUrl.ToString());
            AddHeaders(hotelsRequest);

            Console.WriteLine($"URL запиту готелів: {hotelsUrl}");

            HttpResponseMessage hotelsResponse;
            try
            {
                hotelsResponse = await _httpClient.SendAsync(hotelsRequest);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Помилка HTTP запиту готелів: {e.Message}");
                return Enumerable.Empty<HotelResultDto>();
            }

            Console.WriteLine($"Статус-код відповіді готелів: {hotelsResponse.StatusCode}");
            var hotelJson = await hotelsResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"JSON відповіді готелів: {hotelJson}");

            if (!hotelsResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Помилка API готелів: {hotelsResponse.ReasonPhrase}");
                return Enumerable.Empty<HotelResultDto>();
            }

            BookingHotelSearchResponseDto apiResponse;
            try
            {
                apiResponse = JsonSerializer.Deserialize<BookingHotelSearchResponseDto>(hotelJson);
                if (apiResponse?.Result == null)
                {
                    Console.WriteLine("Не вдалося десеріалізувати відповідь або результат порожній");
                    return Enumerable.Empty<HotelResultDto>();
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Помилка десеріалізації JSON готелів: {ex.Message}");
                return Enumerable.Empty<HotelResultDto>();
            }

            // Фільтрація результатів
            var filteredResults = apiResponse.Result
     .Where(h =>
         (!request.MinPrice.HasValue || (h.PriceBreakdown?.GrossAmount?.Value ?? 0) >= request.MinPrice.Value) &&
         (!request.MaxPrice.HasValue || (h.PriceBreakdown?.GrossAmount?.Value ?? 0) <= request.MaxPrice.Value) &&
         (string.IsNullOrEmpty(request.HotelName) ||
          (h.HotelName?.Contains(request.HotelName, StringComparison.OrdinalIgnoreCase) ?? false)))
     .ToList();

            //Console.WriteLine($"Знайдено {filteredResults.Count} готелів після фільтрації");

            // Мапінг до результатуF
            var result = filteredResults.Select(h => new HotelResultDto
            {
                HotelId = h.HotelId,
                HotelName = h.HotelName,
                Address = h.Address,
                City = request.City,
                Region = request.Country,
                Latitude = h.Latitude,
                Longitude = h.Longitude,
                ReviewScore = h.ReviewScore,
                ReviewCount = h.ReviewCount,
                MainPhotoUrl = h.MainPhotoUrl,
                PriceBreakdown = h.PriceBreakdown,
                Url = h.Url
            }).ToList();

            return result;
        }

        private void AddHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("X-RapidAPI-Key", _apiKey);
            request.Headers.Add("X-RapidAPI-Host", _apiHost);
        }
    }
}