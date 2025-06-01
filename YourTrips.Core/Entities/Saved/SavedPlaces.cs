
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Routes.Saved;

namespace YourTrips.Core.Entities.Saved
{
    public class SavedPlaces : ISavedEntity
    {
        public int Id { get; set; }
        public int RouteId { get; set; }  // Зв'язок з маршрутом, а не з User
        public Route Route { get; set; }
        public string PlaceJson { get; set; } // ID з Booking.com API
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}
