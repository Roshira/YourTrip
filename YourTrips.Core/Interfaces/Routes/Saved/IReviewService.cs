using System.Threading.Tasks;
using YourTrips.Core.DTOs;
using YourTrips.Core.DTOs.Route;

namespace YourTrips.Core.Interfaces.Routes.Saved
{
    /// <summary>
    /// Service interface for handling reviews related to routes.
    /// </summary>
    public interface IReviewService
    {
        /// <summary>
        /// Asynchronously leaves a review for a route.
        /// </summary>
        /// <param name="reviewDto">The data transfer object containing the review details.</param>
        /// <returns>A <see cref="ResultDto"/> representing the result of the review submission.</returns>
        Task<ResultDto> LeaveReviewAsync(ReviewDto reviewDto);
    }
}
