using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking
{
    /// <summary>
    /// Data transfer object representing a hotel suggestion.
    /// </summary>
    public class HotelSuggestionDto
    {
        /// <summary>
        /// Gets or sets the country where the hotel is located.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the city where the hotel is located.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel (optional).
        /// </summary>
        public string? HotelName { get; set; }

        /// <summary>
        /// Gets or sets the destination type (e.g., city, airport, etc.).
        /// </summary>
        public string DestType { get; set; }
    }
}
