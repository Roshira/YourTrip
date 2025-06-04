using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Admin.Data
{
    public class RestaurantSortingResultDto
    {
        public long SequentialTimeMs { get; set; }
        public long ParallelTimeMs { get; set; }
        public int RestaurantCount { get; set; }
        public bool ResultsAreEqual { get; set; }
        public double SpeedupFactor => (double)SequentialTimeMs / ParallelTimeMs;
    }
}
