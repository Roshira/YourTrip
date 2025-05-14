using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Amadeus.Models.Flight
{
    public class FlightSearchResponse
    {
        public List<FlightOffer> Data { get; set; }
        public Meta Meta { get; set; }
        public string? DepartureName { get; set; }
        public string? ArrivalName {  get; set; }
    }
}
