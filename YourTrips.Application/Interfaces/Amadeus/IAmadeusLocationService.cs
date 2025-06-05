using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Interfaces.Interfaces
{
    /// <summary>
    /// Interface for resolving city names and IATA codes using Amadeus data.
    /// </summary>
    public interface IAmadeusLocationService
    {
        /// <summary>
        /// Asynchronously retrieves the IATA airport code based on the given city name.
        /// </summary>
        /// <param name="cityName">The name of the city (e.g., "Paris").</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the IATA code, or null if not found.</returns>
        Task<string?> GetIataCodeFromCityNameAsync(string cityName);

        /// <summary>
        /// Asynchronously retrieves the city name based on the given IATA airport code.
        /// </summary>
        /// <param name="iataCode">The IATA code of the airport (e.g., "CDG").</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the city name, or null if not found.</returns>
        Task<string?> GetCityNameFromIataCodeAsync(string iataCode);
    }
}
