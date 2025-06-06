using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourTrips.Application.DTOs.Admin.Data;
using YourTrips.Core.Interfaces.Admin;

namespace YourTrips.Infrastructure.Services.Admin.Data
{
    /// <summary>
    /// Provides sorting functionality for Paris restaurants using sequential and parallel approaches.
    /// </summary>
    public class ParisRestaurantSorter : IRestaurantSorter
    {
        private readonly IParisRestaurants _parisRestaurants;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParisRestaurantSorter"/> class.
        /// </summary>
        /// <param name="parisRestaurants">Service to retrieve Paris restaurants data.</param>
        public ParisRestaurantSorter(IParisRestaurants parisRestaurants)
        {
            _parisRestaurants = parisRestaurants;
        }

        /// <summary>
        /// Sorts the list of restaurants by rating in descending order sequentially.
        /// If the input list is null or empty, retrieves the full list of Paris restaurants first.
        /// </summary>
        /// <param name="restaurants">The list of restaurants to sort. If null or empty, all Paris restaurants will be loaded.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the sorted list of restaurants.</returns>
        public async Task<List<RestaurantDto>> SortSequentiallyAsync(List<RestaurantDto> restaurants)
        {
            if (restaurants == null || !restaurants.Any())
            {
                restaurants = await _parisRestaurants.GetParisRestaurantsAsync();
            }

            return restaurants.OrderByDescending(r => r.Rating).ToList();
        }

        /// <summary>
        /// Sorts the list of restaurants by rating in descending order using parallel LINQ.
        /// If the input list is null or empty, retrieves the full list of Paris restaurants first.
        /// </summary>
        /// <param name="restaurants">The list of restaurants to sort. If null or empty, all Paris restaurants will be loaded.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the sorted list of restaurants.</returns>
        public async Task<List<RestaurantDto>> SortInParallelAsync(List<RestaurantDto> restaurants)
        {
            if (restaurants == null || !restaurants.Any())
            {
                restaurants = await _parisRestaurants.GetParisRestaurantsAsync();
            }

            return restaurants.AsParallel()
                              .OrderByDescending(r => r.Rating)
                              .ToList();
        }

        /// <summary>
        /// Compares the execution time and results of sequential and parallel sorting methods on Paris restaurants.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the comparison results including timings and equality of results.</returns>
        public async Task<RestaurantSortingResultDto> CompareSortingMethodsAsync()
        {
            var restaurants = await _parisRestaurants.GetParisRestaurantsAsync();

            // Sequential sorting
            var sequentialStopwatch = System.Diagnostics.Stopwatch.StartNew();
            var sequentialResult = await SortSequentiallyAsync(restaurants);
            sequentialStopwatch.Stop();

            // Parallel sorting
            var parallelStopwatch = System.Diagnostics.Stopwatch.StartNew();
            var parallelResult = await SortInParallelAsync(restaurants);
            parallelStopwatch.Stop();

            return new RestaurantSortingResultDto
            {
                SequentialTimeMs = sequentialStopwatch.ElapsedMilliseconds,
                ParallelTimeMs = parallelStopwatch.ElapsedMilliseconds,
                RestaurantCount = restaurants.Count,
                ResultsAreEqual = sequentialResult.SequenceEqual(parallelResult)
            };
        }
    }
}
