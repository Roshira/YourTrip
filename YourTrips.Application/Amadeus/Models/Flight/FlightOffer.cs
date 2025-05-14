using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Amadeus.Models.Flight
{
    public class FlightOffer
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public Price Price { get; set; }
    }
}
