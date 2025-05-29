using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.GoogleMaps.PlaceDetail
{
    public class ReviewDto
    {
        [JsonPropertyName("author_name")]
        public string AuthorName { get; set; }
        public double Rating { get; set; }
        public string Text { get; set; }
    }
}