using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Route.Saved
{
    public class DeleteSavedDto
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string type { get; set; }
    }
}
