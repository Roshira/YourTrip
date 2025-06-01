using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.DTOs;
using YourTrips.Core.DTOs.Route.PartRoutes;

namespace YourTrips.Core.Interfaces.Routes
{

    public interface IRouteService
    {
        Task<ResultDto<RouteDto>> CreateRouteAsync(CreateRouteDto dto, Guid userId);
        Task<ResultDto> DeleteRouteAsync(int routeId);
        Task<ResultDto<List<RouteDto>>> GetAllUserRoutesAsync(Guid userId);
        Task<ResultDto> UpdateImageAsync(string imageUrl, int routeId);
        Task<ShowRouteDto> ShowRouteAsync(int routeId);
    }

}
