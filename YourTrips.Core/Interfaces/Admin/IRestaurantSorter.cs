using System.Collections.Generic;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Admin.Data;

namespace YourTrips.Core.Interfaces.Admin
{
    /// <summary>
    /// Interface for sorting restaurant data using different sorting approaches.
    /// </summary>
    public interface IRestaurantSorter
    {
        /// <summary>
        /// Sorts the list of restaurants sequentially.
        /// </summary>
        /// <param name="restaurants">The list of restaurants to sort.</param>
        /// <returns>A task representing the asynchronous operation, containing the sorted list of restaurants.</returns>
        Task<List<RestaurantDto>> SortSequentiallyAsync(List<RestaurantDto> restaurants);

        /// <summary>
        /// Sorts the list of restaurants in parallel to improve performance.
        /// </summary>
        /// <param name="restaurants">The list of restaurants to sort.</param>
        /// <returns>A task representing the asynchronous operation, containing the sorted list of restaurants.</returns>
        Task<List<RestaurantDto>> SortInParallelAsync(List<RestaurantDto> restaurants);

        /// <summary>
        /// Compares the performance and results of sequential and parallel sorting methods.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing the comparison results in a <see cref="RestaurantSortingResultDto"/>.</returns>
        Task<RestaurantSortingResultDto> CompareSortingMethodsAsync();
    }
}
