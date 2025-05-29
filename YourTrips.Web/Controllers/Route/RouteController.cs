using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.Interfaces.Routes;
using YourTrips.Web.Extensions;

namespace YourTrips.Web.Controllers.Route
{
    [Authorize]
    [Route("api/route")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRouteDto dto)
        {
            var UserId = User.GetUserId();

            var result = await _routeService.CreateRouteAsync(dto, UserId);
            return result.ToApiResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _routeService.DeleteRouteAsync(id);
            return result.ToApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetUserId();
            var result = await _routeService.GetAllUserRoutesAsync(userId);
            return result.ToApiResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _routeService.GetRouteDetailsAsync(id);
            return result.ToApiResponse();
        }
    }
}