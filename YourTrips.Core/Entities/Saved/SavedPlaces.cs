using System;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Core.Entities.Saved
{
    /// <summary>
    /// Represents a saved place associated with a travel route.
    /// Stores place information as JSON, e.g., from Booking.com API.
    /// </summary>
    public class SavedPlaces : ISavedEntity
    {
        /// <summary>
        /// Primary key of the saved place entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key linking to the related Route.
        /// </summary>
        public int RouteId { get; set; }

        /// <summary>
        /// Navigation property to the associated Route.
        /// </summary>
        public Route Route { get; set; }

        /// <summary>
        /// JSON string containing place data (e.g., from Booking.com API).
        /// </summary>
        public string PlaceJson { get; set; }

        /// <summary>
        /// UTC timestamp when this place was saved.
        /// </summary>
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}
