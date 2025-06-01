using System;
using System.Collections.Generic;

namespace YourTrips.Core.DTOs.Route
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
        public bool IsCompleted { get; set; }
       
    }

}