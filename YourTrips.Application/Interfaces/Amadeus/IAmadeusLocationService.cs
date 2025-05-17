using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Interfaces.Interfaces
{
    public interface IAmadeusLocationService
    {
        Task<string?> GetIataCodeFromCityNameAsync(string cityName);

        Task<string?> GetCityNameFromIataCodeAsync(string iataCode);
    }
}
