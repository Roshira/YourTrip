using System;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Core.Entities.Saved
{
    /// <summary>
    /// Represents a saved hotel associated with a travel route.
    /// Stores hotel information as JSON, e.g., from a hotel booking API.
    /// </summary>
    public class SavedHotel : ISavedEntity
    {
        /// <summary>
        /// Primary key of the saved hotel entry.
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
        /// JSON string containing hotel data.
        /// </summary>
        public string HotelJson { get; set; }

        /// <summary>
        /// UTC timestamp when this hotel was saved.
        /// </summary>
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}
