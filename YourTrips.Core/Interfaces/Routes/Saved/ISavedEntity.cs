using System;
using YourTrips.Core.Entities;

namespace YourTrips.Core.Interfaces.Routes.Saved
{
    /// <summary>
    /// Interface representing an entity that is saved as part of a route.
    /// </summary>
    public interface ISavedEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the saved entity.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the associated route.
        /// Note: This links the saved entity to a route, not directly to a user.
        /// </summary>
        int RouteId { get; set; }

        /// <summary>
        /// Gets or sets the associated route entity.
        /// </summary>
        Route Route { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the entity was saved.
        /// </summary>
        DateTime SavedAt { get; set; }
    }
}
