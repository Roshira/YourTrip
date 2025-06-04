using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces;
using YourTrips.Core.DTOs.Route.PartRoutes;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Routes;
using YourTrips.Web.Extensions;
using YourTrips.Core.Interfaces.Achievements;

namespace YourTrips.Web.Controllers.Route
{
    [Authorize]
    [Route("api/route")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        private readonly ICreateAndShowAdchivement _achievements;
        public RouteController(IRouteService routeService, ICreateAndShowAdchivement achievement)
        {
            _routeService = routeService;
            _achievements = achievement;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRouteDto dto)
        {
            var userId = User.GetUserId();

            var result = await _routeService.CreateRouteAsync(dto, userId);
            return result.ToApiResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _routeService.DeleteRouteAsync(id);
            var userId = User.GetUserId();
            await _achievements.CheckAchievementAsync(userId);
            return result.ToApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetUserId();
            var result = await _routeService.GetAllUserRoutesAsync(userId);
            return result.ToApiResponse();
        }

        [HttpPost("updateImage")]
        public async Task<IActionResult> UpdateImage(string imageUrl, int id)
        {
            var result = await _routeService.UpdateImageAsync(imageUrl, id);
            return result.ToApiResponse();
        }
        [HttpGet("showRoute")]
        public async Task<IActionResult> ShowRoute(int routeId)
        {
            var result = await _routeService.ShowRouteAsync(routeId);
            if (result == null) return NotFound("Маршрут не знайдено");

            return Ok(result);
        }
    }
}