using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Amadeus.Flight
{
    public class TravelerDto
    {
        public string Id { get; set; }
        public string TravelerType { get; set; }  // ADULT, CHILD, INFANT і т.д.
    }
}
