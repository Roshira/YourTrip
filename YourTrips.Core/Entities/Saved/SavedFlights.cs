using System;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Core.Entities.Saved
{
    /// <summary>
    /// Represents saved flight details associated with a travel route.
    /// Stores flight information as JSON, for example, from a flight booking API.
    /// </summary>
    public class SavedFlights : ISavedEntity
    {
        /// <summary>
        /// Primary key of the saved flight entry.
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
        /// JSON string containing flight data.
        /// </summary>
        public string FlightsJson { get; set; }

        /// <summary>
        /// UTC timestamp when this flight was saved.
        /// </summary>
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}
