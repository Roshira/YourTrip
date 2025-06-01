using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route
{
    public class ReviewDto
    {
        public int RouteId { get; set; }
        public string? Review { get; set; }
        public double? Rating { get; set; }
    }
}
