using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.Entities
{
    public class TripHistory
    {
        public int Id { get; set; }
        public bool Finished { get; set; } = false;
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public string ExternalFlightId { get; set; }
        public string ExternalHotelId { get; set; }
        public string ExternalBlaBlaCarTripId { get; set; }
        public string ExternalPlaceId { get; set; }
        public string ExternalTrainTripId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
