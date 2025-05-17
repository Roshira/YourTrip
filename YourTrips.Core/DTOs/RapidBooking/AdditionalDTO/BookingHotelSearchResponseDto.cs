using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking.AdditionalDTO
{
    public class BookingHotelSearchResponseDto
    {
        [JsonPropertyName("result")]
        public List<BookingHotelResult> Result { get; set; }
    }
}
