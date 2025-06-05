using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route.PartRoutes
{
    /// <summary>
    /// Data transfer object representing a saved item associated with a route.
    /// </summary>
    public class SavedItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the saved item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the saved item (e.g., "hotel", "flight").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the item was saved.
        /// </summary>
        public DateTime SavedAt { get; set; }
    }
}
