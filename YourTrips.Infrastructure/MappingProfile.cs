using AutoMapper;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.Entities;

namespace YourTrips.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Route, RouteDto>();
            CreateMap<RouteDto, Route>(); // Якщо треба в зворотному напрямку
        }
    }
}
