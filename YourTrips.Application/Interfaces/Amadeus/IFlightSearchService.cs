using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Amadeus.Models.Flight;
using YourTrips.Application.DTOs.Amadeus.Flight;

namespace YourTrips.Application.Interfaces.Interfaces
{
    /// <summary>
    /// Interface for searching flights using the Amadeus flight search API.
    /// </summary>
    public interface IFlightSearchService
    {
        /// <summary>
        /// Asynchronously searches for flights based on the provided parameters.
        /// </summary>
        /// <param name="origin">The IATA code of the origin location (e.g., "LHR").</param>
        /// <param name="destination">The IATA code of the destination location (e.g., "JFK").</param>
        /// <param name="departureDate">The date of departure.</param>
        /// <param name="Travelers">A list of travelers for the flight search, including type and quantity.</param>
        /// <param name="cabin">The desired cabin class (e.g., "ECONOMY", "BUSINESS").</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the flight search response.</returns>
        Task<FlightSearchResponse> SearchFlightsAsync(
            string origin,
            string destination,
            DateTime departureDate,
            List<TravelerDto> Travelers,
            string cabin
        );
    }
}
