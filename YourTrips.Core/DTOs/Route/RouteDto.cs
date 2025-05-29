using System;
using System.Collections.Generic;

namespace YourTrips.Core.DTOs.Route
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class CreateRouteDto
    {
        public string Name { get; set; }
    }

    public class RouteDetailsDto : RouteDto
    {
        public string Review { get; set; }
        public double? Rating { get; set; }
        public List<SavedItemDto> SavedItems { get; set; }
    }

    public class SavedItemDto
    {
        public int Id { get; set; }
        public string Type { get; set; } // "hotel", "flight", etc.
        public DateTime SavedAt { get; set; }
    }
}