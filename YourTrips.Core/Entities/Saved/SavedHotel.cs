using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Interfaces.Saved;

namespace YourTrips.Core.Entities.Saved
{

    public class SavedHotel : ISavedEntity
    {
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string HotelJson { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    public User User { get; set; }

    }
    
}


