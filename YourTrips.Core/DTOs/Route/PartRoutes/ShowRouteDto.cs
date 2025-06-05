using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.GoogleMaps;
using YourTrips.Core.DTOs.Route.Saved;

namespace YourTrips.Core.DTOs.Route.PartRoutes
{
    /// <summary>
    /// Data transfer object for displaying a route with its saved items in JSON format.
    /// </summary>
    public class ShowRouteDto
    {
        /// <summary>
        /// Gets or sets the list of saved items associated with the route, stored as JSON.
        /// </summary>
        public List<SavedDto> SavedJson { get; set; }
    }
}
