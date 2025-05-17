using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking.AdditionalDTO
{
    public class BookingLocationDto
    {
        [JsonPropertyName("dest_id")]
        public string DestId { get; set; }

        [JsonPropertyName("dest_type")]
        public string DestType { get; set; }
    }

}
