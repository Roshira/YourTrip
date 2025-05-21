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
    public class BookingDescribeService : IBookingDescribeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiHost;

        public BookingDescribeService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["RapidBooking:ApiKey"];
            _apiHost = config["RapidBooking:BaseUrl"];
        }

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
