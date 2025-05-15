using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Infrastructure.Services.BookingService
{
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using global::YourTrips.Application.RapidBooking.Interfaces;
    using global::YourTrips.Core.DTOs.RapidBooking;

    namespace YourTrips.Infrastructure.RapidBooking.Services
    {
        public class BookingApiService : IBookingApiService
        {
            private readonly HttpClient _httpClient;
            private const string ApiHost = "booking-com.p.rapidapi.com";
            private const string ApiKey = "your-rapidapi-key"; // 🔒 Замінити на свій ключ

            public BookingApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<IEnumerable<HotelResultDto>> SearchHotelsAsync(HotelSearchRequestDto request)
            {
                // 1. Отримуємо destination_id (місто) — це окремий запит!
                var locationUrl = $"https://{ApiHost}/v1/hotels/locations?name={request.City}&locale=en-us";
                var locationRequest = new HttpRequestMessage(HttpMethod.Get, locationUrl);
                locationRequest.Headers.Add("X-RapidAPI-Key", ApiKey);
                locationRequest.Headers.Add("X-RapidAPI-Host", ApiHost);

                var locationResponse = await _httpClient.SendAsync(locationRequest);
                if (!locationResponse.IsSuccessStatusCode)
                    return Enumerable.Empty<HotelResultDto>();

                var locationJson = await locationResponse.Content.ReadAsStringAsync();
                var locationData = JsonSerializer.Deserialize<List<BookingLocationDto>>(locationJson);

                var destinationId = locationData?.FirstOrDefault()?.DestId;
                if (string.IsNullOrEmpty(destinationId))
                    return Enumerable.Empty<HotelResultDto>();

                // 2. Запит на пошук готелів
                var checkIn = request.CheckIn.ToString("yyyy-MM-dd");
                var checkOut = request.CheckOut.ToString("yyyy-MM-dd");

                var hotelsUrl = $"https://{ApiHost}/v1/hotels/search?dest_id={destinationId}&dest_type=city&checkin_date={checkIn}&checkout_date={checkOut}&adults_number={request.Adults}&order_by=popularity&locale=en-us&price_filter_currencycode=USD&units=metric&room_number=1&filter_by_currency=USD";

                var hotelsRequest = new HttpRequestMessage(HttpMethod.Get, hotelsUrl);
                hotelsRequest.Headers.Add("X-RapidAPI-Key", ApiKey);
                hotelsRequest.Headers.Add("X-RapidAPI-Host", ApiHost);

                var hotelsResponse = await _httpClient.SendAsync(hotelsRequest);
                if (!hotelsResponse.IsSuccessStatusCode)
                    return Enumerable.Empty<HotelResultDto>();

                var hotelJson = await hotelsResponse.Content.ReadAsStringAsync();
                var hotelData = JsonSerializer.Deserialize<BookingHotelSearchResponseDto>(hotelJson);

                // 3. Мапимо результат
                var result = hotelData?.Result?
                    .Where(h => h.Price >= request.MinPrice && h.Price <= request.MaxPrice)
                    .Select(h => new HotelResultDto
                    {
                        Name = h.HotelName,
                        Address = h.Address,
                        City = request.City,
                        Latitude = h.Latitude,
                        Longitude = h.Longitude,
                        Description = h.Description,
                        Rating = h.ReviewScore,
                        ReviewCount = h.ReviewCount,
                        ImageUrl = h.MainPhotoUrl,
                        PricePerNight = (decimal)h.Price,
                        Currency = h.Currency,
                        BookingUrl = h.Url
                    });

                return result ?? Enumerable.Empty<HotelResultDto>();
            }
        }
    }

}
