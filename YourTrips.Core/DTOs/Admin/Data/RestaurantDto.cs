using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Admin.Data
{
    public class RestaurantDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public int PriceLevel { get; set; } // 0 - невідомо, 1-4 - рівні цін
        public List<string> PhotoUrls { get; set; }
        public List<string> Types { get; set; }
        public bool IsOpenNow { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
    }
    public class RestaurantDetailsDto
    {
        public OpeningHoursDto OpeningHours { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
    }

    public class OpeningHoursDto
    {
        public bool OpenNow { get; set; }
    }
}
