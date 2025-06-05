using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.RapidBooking.Describe;

namespace YourTrips.Application.Interfaces.Interfaces
{
    /// <summary>
    /// Provides hotel description services based on search criteria.
    /// </summary>
    public interface IBookingDescribeService
    {
        /// <summary>
        /// Asynchronously retrieves detailed description information for a hotel based on the specified request.
        /// </summary>
        /// <param name="request">The request containing hotel search criteria.</param>
        /// <returns>A task representing the asynchronous operation, containing detailed hotel description results.</returns>
        Task<HotelDescribeResultDto> DescribeHotelAsync(HotelSearchDescribeDto request);
    }
}
