using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.Amadeus
{
    public class LocationData
    {
        public string Type { get; set; } = default!;
        public string SubType { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string IataCode { get; set; } = default!;
    }
}
