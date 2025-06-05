using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Amadeus.Locations;

namespace YourTrips.Application.Interfaces.Amadeus
{
    /// <summary>
    /// Interface for retrieving location suggestions using the Amadeus API.
    /// </summary>
    public interface ISuggestAmadeusService
    {
        /// <summary>
        /// Asynchronously retrieves a list of suggested locations based on the provided search query.
        /// </summary>
        /// <param name="query">The search query string (e.g., city name, airport name).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of suggested locations.</returns>
        Task<List<LocationData>> GetLocationSuggestions(string query);
    }
}
