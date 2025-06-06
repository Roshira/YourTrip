using System;
using System.Collections.Generic;

namespace YourTrips.Application.DTOs.Route
{
    /// <summary>
    /// Data transfer object representing a travel route.
    /// </summary>
    public class RouteDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the route.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the route.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the review text for the route.
        /// </summary>
        public string Review { get; set; }

        /// <summary>
        /// Gets or sets the average rating of the route.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the route was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the URL of the route's main image.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the route is completed.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
