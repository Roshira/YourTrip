using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route.PartRoutes
{
    /// <summary>
    /// Data transfer object used to create a new route.
    /// </summary>
    public class CreateRouteDto
    {
        /// <summary>
        /// Gets or sets the name of the route to be created.
        /// </summary>
        public string Name { get; set; }
    }
}
