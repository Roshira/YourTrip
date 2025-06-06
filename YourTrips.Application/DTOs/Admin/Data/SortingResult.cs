using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.Admin.Data
{
    public class SortingResult
    {
        public List<UserRoutes> UserRoutes { get; set; }
        public double ExecutionTimeMs { get; set; }
        public string MethodName { get; set; }
    }
}
