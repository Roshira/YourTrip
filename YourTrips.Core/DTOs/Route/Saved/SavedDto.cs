using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route.Saved
{
    public class SavedDto
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string Json { get; set; }
        public string? Type { get; set; }
    }
}
