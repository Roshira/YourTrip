using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.GoogleMaps.PlaceDetail.ResponseClase
{
    public class OpeningHours
    {
        public bool? Open_Now { get; set; }
        public List<string> Weekday_Text { get; set; }
    }

}
