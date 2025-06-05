using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Amadeus.Locations
{
    /// <summary>
    /// Represents the response containing a list of location data from the Amadeus API.
    /// </summary>
    public class LocationResponse
    {
        /// <summary>
        /// Gets or sets the list of location data entries.
        /// </summary>
        public List<LocationData>? Data { get; set; }
    }
}
