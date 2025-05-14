using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Amadeus.Models.Flight
{
    public class Segment
    {
        public Airport Departure { get; set; }
        public Airport Arrival { get; set; }
        public string CarrierCode { get; set; }
        public string Number { get; set; }
    }
}
