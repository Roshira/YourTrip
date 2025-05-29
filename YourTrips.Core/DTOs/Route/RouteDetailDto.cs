using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Route.PartRoutes;

namespace YourTrips.Core.DTOs.Route
{
    public class RouteDetailsDto : RouteDto
    {
        public string Review { get; set; }
        public double? Rating { get; set; }
        public List<SavedItemDto> SavedItems { get; set; }
    }
}
