using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;

namespace YourTrips.Core.DTOs.Admin.Data
{
    public class SortingResult
    {
        public List<UserRoutes> UserRoutes { get; set; }
        public long ExecutionTimeMs { get; set; }
        public string MethodName { get; set; }
    }
}
