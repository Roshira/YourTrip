using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking
{
    public class HotelSearchRequestDto
    {
        public string City { get; set; }              // Наприклад: "Paris"
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Adults { get; set; }               // Наприклад: 2
        public decimal? MinPrice { get; set; }        // Наприклад: 50
        public decimal? MaxPrice { get; set; }        // Наприклад: 200
    }
}
