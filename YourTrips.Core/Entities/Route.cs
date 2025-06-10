using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using YourTrips.Core.Entities.Saved;

namespace YourTrips.Core.Entities
{
    /// <summary>
    /// Represents a travel route created by a user.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Primary key of the route.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the user who owns the route.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation property to the user who owns this route.
        /// Marked to be ignored during JSON serialization.
        /// </summary>
        [JsonIgnore]
        public User User { get; set; }

        /// <summary>
        /// Optional name of the route.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates whether the route has been completed.
        /// </summary>
        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// Optional user review or description of the route.
        /// </summary>
        public string? Review { get; set; }

        /// <summary>
        /// Optional rating of the route, typically from 1 to 5.
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        /// URL of an image representing the route.
        /// Defaults to a placeholder image URL.
        /// </summary>
        public string ImageUrl { get; set; } = "https://i0.wp.com/www.bishoprook.com/wp-content/uploads/2021/05/placeholder-image-gray-16x9-1.png?ssl=1";

        /// <summary>
        /// The UTC date and time when the route was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Collection of saved hotels associated with the route.
        /// </summary>
        public ICollection<SavedHotel> SavedHotels { get; set; }

        /// <summary>
        /// Collection of saved flights associated with the route.
        /// </summary>
        public ICollection<SavedFlights> SavedFlights { get; set; }

        /// <summary>
        /// Collection of saved places associated with the route.
        /// </summary>
        public ICollection<SavedPlaces> SavedPlaces { get; set; }
    }
}
