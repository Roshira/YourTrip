using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route
{
    /// <summary>
    /// Data transfer object representing a review for a route.
    /// </summary>
    public class ReviewDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the associated route.
        /// </summary>
        public int RouteId { get; set; }

        /// <summary>
        /// Gets or sets the text of the review.
        /// </summary>
        public string? Review { get; set; }

        /// <summary>
        /// Gets or sets the rating given in the review.
        /// Nullable to allow for missing ratings.
        /// </summary>
        public double? Rating { get; set; }
    }
}
