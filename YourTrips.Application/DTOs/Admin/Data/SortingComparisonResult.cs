using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.Admin.Data
{
    public class SortingComparisonResult
    {
        public SortingResult SequentialResult { get; set; }
        public SortingResult ParallelResult { get; set; }
    }
}
