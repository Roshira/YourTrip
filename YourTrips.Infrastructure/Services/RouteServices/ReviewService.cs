using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.Interfaces;
using YourTrips.Core.Interfaces.Routes.Saved;
using YourTrips.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace YourTrips.Infrastructure.Services.RouteServices
{
    public class ReviewService : IReviewService
    {
        private readonly YourTripsDbContext _context;

        public ReviewService(YourTripsDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> LeaveReviewAsync(ReviewDto reviewDto)
        {
            var route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == reviewDto.RouteId);
            if (route == null)
            {
                return ResultDto.Fail("Route not found.");
            }

            route.Review = reviewDto.Review;
            route.Rating = reviewDto.Rating;
            route.IsCompleted = true;

            await _context.SaveChangesAsync();

            return ResultDto.Success("Review submitted and route marked as completed.");
        }
    }
}
