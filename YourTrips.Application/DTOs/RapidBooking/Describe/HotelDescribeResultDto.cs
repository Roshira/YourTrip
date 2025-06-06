using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.RapidBooking.Describe
{
    /// <summary>
    /// Describe list
    /// </summary>
    public class HotelDescribeResultDto
    {
        public List<HotelDescribePurtDto> HotelDescriptions { get; set; } = new();
    }
}
