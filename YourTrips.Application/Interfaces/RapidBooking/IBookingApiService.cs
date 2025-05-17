using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.RapidBooking;

namespace YourTrips.Application.Interfaces.Interfaces
{
    public interface IBookingApiService
    {
        Task<IEnumerable<HotelResultDto>> SearchHotelsAsync(HotelSearchRequestDto request);
    }
}
