using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking
{
    public class HotelSuggestionDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string? HotelName { get; set; }
        public string DestType { get; set; } // ← нове поле
    }
}
