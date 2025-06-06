using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.DTOs.RapidBooking;

namespace YourTrips.Application.Interfaces.Interfaces
{
    /// <summary>
    /// Provides hotel search services using the booking API.
    /// </summary>
    public interface IBookingApiService
    {
        /// <summary>
        /// Asynchronously searches for hotels matching the specified search criteria.
        /// </summary>
        /// <param name="request">The hotel search request parameters.</param>
        /// <returns>A task representing the asynchronous operation, containing a collection of hotel search results.</returns>
        Task<IEnumerable<HotelResultDto>> SearchHotelsAsync(HotelSearchRequestDto request);
    }
}
