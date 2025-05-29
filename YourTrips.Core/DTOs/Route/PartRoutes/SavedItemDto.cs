using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route.PartRoutes
{
    public class SavedItemDto
    {
        public int Id { get; set; }
        public string Type { get; set; } // "hotel", "flight", etc.
        public DateTime SavedAt { get; set; }
    }
}
