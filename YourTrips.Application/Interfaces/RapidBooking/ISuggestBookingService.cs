using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.RapidBooking;

namespace YourTrips.Application.Interfaces.RapidBooking
{
    /// <summary>
    /// Defines a service for retrieving hotel booking suggestions based on a search term.
    /// </summary>
    public interface ISuggestBookingService
    {
        /// <summary>
        /// Asynchronously gets a collection of hotel suggestions matching the specified search term.
        /// </summary>
        /// <param name="searchTerm">The search term to find hotel suggestions for.</param>
        /// <returns>A task representing the asynchronous operation, containing an enumerable of hotel suggestions.</returns>
        public Task<IEnumerable<HotelSuggestionDto>> GetSuggestionsAsync(string searchTerm);
    }
}
