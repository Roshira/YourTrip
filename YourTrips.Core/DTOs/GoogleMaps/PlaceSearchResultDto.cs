using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.GoogleMaps
{
    /// <summary>
    /// Data transfer object representing a place search result from Google Maps.
    /// </summary>
    public class PlaceSearchResultDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the place.
        /// </summary>
        [JsonPropertyName("place_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the place.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the place.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the URL(s) of photos related to the place.
        /// </summary>
        [JsonPropertyName("photoUrl")]
        public string PhotoUrls { get; set; }
    }
}
