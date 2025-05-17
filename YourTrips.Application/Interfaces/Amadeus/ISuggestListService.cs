using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Amadeus.Locations;

namespace YourTrips.Application.Interfaces.Amadeus
{
    public interface ISuggestListService
    {
        Task<List<LocationData>> GetLocationSuggestions(string query);
    }
}
