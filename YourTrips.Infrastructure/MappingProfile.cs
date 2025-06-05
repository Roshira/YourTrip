using AutoMapper;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.Entities;

namespace YourTrips.Infrastructure
{
    /// <summary>
    /// AutoMapper profile for mapping between domain entities and data transfer objects (DTOs).
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// Configures mapping between <see cref="Route"/> and <see cref="RouteDto"/>.
        /// </summary>
        public MappingProfile()
        {
            // Map from Route entity to RouteDto
            CreateMap<Route, RouteDto>();

            // Map from RouteDto back to Route entity (optional, useful for create/update scenarios)
            CreateMap<RouteDto, Route>();
        }
    }
}
