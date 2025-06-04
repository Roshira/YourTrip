// ParisRestaurantSorter.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Admin.Data;
using YourTrips.Core.Interfaces.Admin;

namespace YourTrips.Infrastructure.Services.Admin.Data
{
    public class ParisRestaurantSorter : IRestaurantSorter
    {
        private readonly IParisRestaurants _parisRestaurants;

        public ParisRestaurantSorter(IParisRestaurants parisRestaurants)
        {
            _parisRestaurants = parisRestaurants;
        }

        public async Task<List<RestaurantDto>> SortSequentiallyAsync(List<RestaurantDto> restaurants)
        {
            if (restaurants == null || !restaurants.Any())
            {
                restaurants = await _parisRestaurants.GetParisRestaurantsAsync();
            }

            return restaurants.OrderByDescending(r => r.Rating).ToList();
        }

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
