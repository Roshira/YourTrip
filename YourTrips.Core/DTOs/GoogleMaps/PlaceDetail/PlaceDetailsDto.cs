using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.GoogleMaps.PlaceDetail
{
    public class PlaceDetailsDto
    {
        public string Name { get; set; }
        public string Formatted_Address { get; set; }
        public string Vicinity { get; set; }
        public string Phone { get; set; }
        public string InternationalPhoneNumber { get; set; }
        public string Website { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public List<string> PhotoUrl { get; set; }
        public List<string> Reviews { get; set; }
        public bool? Opening_Now { get; set; }
        public List<string> WeekdayText { get; set; }
        public double? Rating { get; set; }
        public int? UserRatingsTotal { get; set; }
        public List<string> Types { get; set; }

        [JsonPropertyName("price_level")]
        public int? Price_Level { get; set; }
        public string Url { get; set; }
        public string BusinessStatus { get; set; }
        public string Summary { get; set; }
    }


}
