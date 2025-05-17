using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Amadeus.Models.Flight;
using YourTrips.Core.DTOs.Amadeus.Flight;

namespace YourTrips.Application.Interfaces.Interfaces
{
    public interface IFlightSearchService
    {
        Task<FlightSearchResponse> SearchFlightsAsync(
            string origin,
            string destination,
            DateTime departureDate,
            List<TravelerDto> Travelers,
            string cabin
            );
    }
}
