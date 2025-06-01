using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.GoogleMaps;
using YourTrips.Core.DTOs.Route.Saved;

namespace YourTrips.Core.DTOs.Route.PartRoutes
{
    public class ShowRouteDto
    {
        public List<SavedDto> SavedJson { get; set; }
    }
}
