using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Interfaces.Interfaces
{
    /// <summary>
    /// Interface for obtaining an access token from the Amadeus API.
    /// </summary>
    public interface IAmadeusAuthService
    {
        /// <summary>
        /// Asynchronously retrieves an access token required for making requests to the Amadeus API.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the access token string.</returns>
        Task<string> GetAccessTokenAsync();
    }
}
