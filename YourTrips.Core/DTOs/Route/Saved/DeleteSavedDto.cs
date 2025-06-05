using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route.Saved
{
    /// <summary>
    /// Data transfer object used to request deletion of a saved item related to a route.
    /// </summary>
    public class DeleteSavedDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the saved item to be deleted.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated route.
        /// </summary>
        public int RouteId { get; set; }

        /// <summary>
        /// Gets or sets the type of the saved item (e.g., hotel, flight, place).
        /// </summary>
        public string Type { get; set; }
    }
}
