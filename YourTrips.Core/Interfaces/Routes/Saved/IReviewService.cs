using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;
using YourTrips.Core.DTOs.Route;

namespace YourTrips.Core.Interfaces.Routes.Saved
{
    public interface IReviewService
    {
        Task<ResultDto> LeaveReviewAsync(ReviewDto reviewDto);
    }
}
