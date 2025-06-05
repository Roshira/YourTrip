using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route.Saved
{
    /// <summary>
    /// Data transfer object representing a saved item related to a route.
    /// </summary>
    public class SavedDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the saved item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated route.
        /// </summary>
        public int RouteId { get; set; }

        /// <summary>
        /// Gets or sets the saved item data in JSON format.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Gets or sets the type of the saved item (optional).
        /// </summary>
        public string? Type { get; set; }
    }
}
