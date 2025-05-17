using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.RapidBooking;

namespace YourTrips.Application.Interfaces.Interfaces
{
    public interface IBookingDescribeService
    {
        Task<HotelDescribeResultDto> DescribeHotelAsync(HotelSearchDescribeDto request);
    }
}
