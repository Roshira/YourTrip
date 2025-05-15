using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Amadeus.Flight;

namespace YourTrips.Core.DTOs.Amadeus
{
    public class FlightSearchRequestDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public string Cabin { get; set; }
        public List<TravelerDto> Travellers { get; set; }
    }
}
