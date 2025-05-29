using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Core.Entities.Saved
{

    public class SavedHotel : ISavedEntity
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public string HotelJson { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    }

}


