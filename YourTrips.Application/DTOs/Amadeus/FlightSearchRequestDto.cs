using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.DTOs.Amadeus.Flight;

namespace YourTrips.Application.DTOs.Amadeus
{
    /// <summary>
    /// for search flights
    /// </summary>
    public class FlightSearchRequestDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public string Cabin { get; set; }
        public List<TravelerDto> Travellers { get; set; }
    }
}
