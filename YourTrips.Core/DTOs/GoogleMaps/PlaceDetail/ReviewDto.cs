using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.GoogleMaps.PlaceDetail
{
    /// <summary>
    /// Data transfer object representing a review for a place.
    /// </summary>
    public class ReviewDto
    {
        /// <summary>
        /// Gets or sets the name of the author who wrote the review.
        /// </summary>
        [JsonPropertyName("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the rating given by the author.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the text content of the review.
        /// </summary>
        public string Text { get; set; }
    }
}
