using System.Collections.Generic;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Admin.Data;

namespace YourTrips.Core.Interfaces.Admin
{
    /// <summary>
    /// Interface for retrieving a list of restaurants located in Paris.
    /// </summary>
    public interface IParisRestaurants
    {
        /// <summary>
        /// Asynchronously gets the list of restaurants in Paris.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a list of <see cref="RestaurantDto"/> representing Paris restaurants.</returns>
        Task<List<RestaurantDto>> GetParisRestaurantsAsync();
    }
}
