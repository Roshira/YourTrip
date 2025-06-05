using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.GoogleMaps.PlaceDetail
{
    /// <summary>
    /// Data transfer object representing detailed information about a place from Google Maps.
    /// </summary>
    public class PlaceDetailsDto
    {
        /// <summary>
        /// Gets or sets the name of the place.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the formatted address of the place.
        /// </summary>
        public string Formatted_Address { get; set; }

        /// <summary>
        /// Gets or sets the vicinity or nearby area description.
        /// </summary>
        public string Vicinity { get; set; }

        /// <summary>
        /// Gets or sets the local phone number of the place.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the international phone number of the place.
        /// </summary>
        public string InternationalPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the website URL of the place.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate of the place.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate of the place.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets a list of photo URLs associated with the place.
        /// </summary>
        public List<string> PhotoUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of reviews for the place.
        /// </summary>
        public List<ReviewDto> Reviews { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the place is currently open.
        /// </summary>
        public bool? Opening_Now { get; set; }

        /// <summary>
        /// Gets or sets the list of textual descriptions of opening hours by weekday.
        /// </summary>
        public List<string> WeekdayText { get; set; }

        /// <summary>
        /// Gets or sets the average user rating of the place.
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        /// Gets or sets the total number of user ratings.
        /// </summary>
        public int? UserRatingsTotal { get; set; }

        /// <summary>
        /// Gets or sets the list of place types (categories).
        /// </summary>
        public List<string> Types { get; set; }

        /// <summary>
        /// Gets or sets the price level of the place (e.g., 0 to 4).
        /// </summary>
        [JsonPropertyName("price_level")]
        public int? Price_Level { get; set; }

        /// <summary>
        /// Gets or sets the URL of the place’s Google Maps page.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the business status (e.g., operational, closed temporarily).
        /// </summary>
        public string BusinessStatus { get; set; }

        /// <summary>
        /// Gets or sets a summary or description of the place.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the type identifier for this DTO (default is "Place").
        /// </summary>
        public string Type { get; set; } = "Place";
    }
}
