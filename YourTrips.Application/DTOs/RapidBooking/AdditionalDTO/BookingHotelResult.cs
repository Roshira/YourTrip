using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.RapidBooking.AdditionalDTO
{
    /// <summary>
    /// It it result with get frontend and we read
    /// </summary>
    public class BookingHotelResult
    {
        [JsonPropertyName("hotel_id")]
        public int HotelId { get; set; }
        [JsonPropertyName("hotel_name")]
        public string HotelName { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; } 

        [JsonPropertyName("review_score")]
        public double ReviewScore { get; set; }

        [JsonPropertyName("review_nr")]
        public int ReviewCount { get; set; }

        [JsonPropertyName("main_photo_url")]
        public string MainPhotoUrl { get; set; }

        [JsonPropertyName("composite_price_breakdown")]
        public PriceBreakdown PriceBreakdown { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

    }

}
