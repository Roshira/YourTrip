using System.Threading.Tasks;
using YourTrips.Core.DTOs.Admin.Data;

namespace YourTrips.Core.Interfaces.Admin
{
    /// <summary>
    /// Service interface for comparing different user sorting methods.
    /// </summary>
    public interface IUserSortingService
    {
        /// <summary>
        /// Asynchronously compares various sorting algorithms or methods for users.
        /// </summary>
        /// <returns>
        /// A <see cref="SortingComparisonResult"/> containing the comparison details and results.
        /// </returns>
        Task<SortingComparisonResult> CompareSortingMethods();
    }
}
