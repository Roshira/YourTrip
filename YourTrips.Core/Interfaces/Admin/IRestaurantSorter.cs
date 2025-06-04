using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Admin.Data;

namespace YourTrips.Core.Interfaces.Admin
{
    public interface IRestaurantSorter
    {
        Task<List<RestaurantDto>> SortSequentiallyAsync(List<RestaurantDto> restaurants);
        Task<List<RestaurantDto>> SortInParallelAsync(List<RestaurantDto> restaurants);
        Task<RestaurantSortingResultDto> CompareSortingMethodsAsync();
    }
}
