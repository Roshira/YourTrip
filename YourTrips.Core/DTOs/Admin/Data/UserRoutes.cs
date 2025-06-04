using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;

namespace YourTrips.Core.DTOs.Admin.Data
{
    public class UserRoutes
    {
        public User User { get; set; }
        public int RoutesCount { get; set; }
    }
}
