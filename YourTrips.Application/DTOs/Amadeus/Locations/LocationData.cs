using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.Amadeus.Locations
{
    /// <summary>
    /// Represents location data information such as airports, cities, or other location types.
    /// </summary>
    public class LocationData
    {
        /// <summary>
        /// Gets or sets the general type of the location (e.g., "airport", "city").
        /// </summary>
        public string Type { get; set; } = default!;

        /// <summary>
        /// Gets or sets the subtype of the location (e.g., "airport", "train station").
        /// </summary>
        public string SubType { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the IATA code associated with the location.
        /// </summary>
        public string IataCode { get; set; } = default!;
    }
}
