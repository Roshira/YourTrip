using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking
{
    /// <summary>
    /// Data transfer object representing a hotel search request.
    /// </summary>
    public class HotelSearchRequestDto
    {
        /// <summary>
        /// Gets or sets the country where to search for hotels.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the city where to search for hotels.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the optional name of the hotel to search for.
        /// </summary>
        public string? HotelName { get; set; }

        /// <summary>
        /// Gets or sets the check-in date for the hotel stay.
        /// </summary>
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// Gets or sets the check-out date for the hotel stay.
        /// </summary>
        public DateTime CheckOutDate { get; set; }

        /// <summary>
        /// Gets or sets the number of adults staying (default is 1).
        /// </summary>
        public int Adults { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of children staying (default is 0).
        /// </summary>
        public int Children { get; set; } = 0;

        /// <summary>
        /// Gets or sets the minimum price filter for the hotel search (optional).
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Gets or sets the maximum price filter for the hotel search (optional).
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Gets or sets the number of rooms required (default is 1).
        /// </summary>
        public int Rooms { get; set; } = 1;
    }
}
