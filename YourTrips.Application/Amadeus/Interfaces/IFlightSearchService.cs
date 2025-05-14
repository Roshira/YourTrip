using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Amadeus.Models.Flight;

namespace YourTrips.Application.Amadeus.Interfaces
{
    public interface IFlightSearchService
    {
        Task<FlightSearchResponse> SearchFlightsAsync(
            string origin,
            string destination,
            DateTime departureDate,
            int passengers);
    }
}
