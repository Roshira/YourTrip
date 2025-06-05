using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Core.DTOs.RapidBooking.Describe;

namespace YourTrips.Infrastructure.Services.BookingService
{
    /// <summary>
    /// Service responsible for retrieving hotel descriptions using the RapidAPI Booking.com API.
    /// </summary>
    public class BookingDescribeService : IBookingDescribeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingDescribeService"/> class.
        /// </summary>
        /// <param name="httpClient">Injected HTTP client instance used to send requests.</param>
        /// <param name="config">Application configuration used to retrieve API keys and host information.</param>
        public BookingDescribeService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["RapidBooking:ApiKey"];
            _apiHost = config["RapidBooking:BaseUrl"];
        }

        /// <summary>
        /// Retrieves hotel descriptions for up to four specified hotel IDs from the Booking.com API via RapidAPI.
        /// </summary>
        /// <param name="request">An object containing up to four hotel IDs to describe.</param>
        /// <returns>
        /// A <see cref="HotelDescribeResultDto"/> object containing hotel IDs and their corresponding descriptions.
        /// </returns>
        /// <exception cref="HttpRequestException">Thrown if the HTTP request fails or returns a non-success status code.</exception>
        public async Task<HotelDescribeResultDto> DescribeHotelAsync(HotelSearchDescribeDto request)
        {
            var resultDto = new HotelDescribeResultDto();

            var hotelIds = new List<string?>
            {
                request.HotelId1,
                request.HotelId2,
                request.HotelId3,
                request.HotelId4
            };

            foreach (var hotelId in hotelIds.Where(id => !string.IsNullOrEmpty(id)))
            {
                var url = $"https://{_apiHost}/v1/hotels/description?hotel_id={hotelId}&locale=en-us";

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Add("X-RapidAPI-Key", _apiKey);
                requestMessage.Headers.Add("X-RapidAPI-Host", "booking-com.p.rapidapi.com");

                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                resultDto.HotelDescriptions.Add(new HotelDescribePurtDto
                {
                    HotelId = hotelId,
                    HotelDescribe = jsonResponse?.description ?? "No description available"
                });
            }

            return resultDto;
        }
    }
}
