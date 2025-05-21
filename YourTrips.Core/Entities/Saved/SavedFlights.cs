using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.Entities.Saved
{
    public class SavedFlights
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FlightsJson { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
    }
}
