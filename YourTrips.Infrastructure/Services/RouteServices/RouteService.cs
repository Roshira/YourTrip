using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YourTrips.Application.DTOs;
using YourTrips.Application.DTOs.Route;
using YourTrips.Application.DTOs.Route.PartRoutes;
using YourTrips.Application.DTOs.Route.Saved;
using YourTrips.Application.Interfaces.GoogleMaps;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Routes;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure.Services.Routes
{
    public class RouteService : IRouteService
    {
        private readonly YourTripsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGooglePlacesService _googlePlacesService;

        public RouteService(YourTripsDbContext context, IMapper mapper, IGooglePlacesService googlePlacesService)
        {
            _googlePlacesService = googlePlacesService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<RouteDto>> CreateRouteAsync(CreateRouteDto dto, Guid userId)
        {
            var route = new Route
            {
                UserId = userId,
                Name = string.IsNullOrEmpty(dto.Name)
                    ? $"Мій маршрут {DateTime.Now:dd.MM.yyyy}"
                    : dto.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return ResultDto<RouteDto>.Success(_mapper.Map<RouteDto>(route));
        }

        public async Task<ResultDto> DeleteRouteAsync(int routeId)
        {
            var route = await _context.Routes.FindAsync(routeId);
            if (route == null)
                return ResultDto.Fail("Маршрут не знайдено");

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();

            return ResultDto.Success("Маршрут успішно видалено");
        }

        public async Task<ResultDto<List<RouteDto>>> GetAllUserRoutesAsync(Guid userId)
        {
            var routes = await _context.Routes
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                
                .ToListAsync();

            return ResultDto<List<RouteDto>>.Success(
                _mapper.Map<List<RouteDto>>(routes));
        }

        public async Task<ResultDto> UpdateImageAsync(string imageUrl, int routeId)
        {
            var route = await _context.Routes.FindAsync(routeId);
            if (route == null)
                return ResultDto.Fail("Маршрут не знайдено");

            route.ImageUrl = imageUrl;
            await _context.SaveChangesAsync();  // Save changes to database

            // Assuming you have a method to map Route to RouteDto
            var routeDto = _mapper.Map<RouteDto>(route);  // Or manual mapping

            return ResultDto<RouteDto>.Success(routeDto);
        }
        public async Task<ShowRouteDto> ShowRouteAsync(int routeId)
        {
            var route = await _context.Routes
                .Include(r => r.SavedHotels)
                .Include(r => r.SavedFlights)
                .Include(r => r.SavedPlaces)
                .FirstOrDefaultAsync(r => r.Id == routeId);

            if (route == null) return null;

            var savedList = new List<SavedDto>();

            savedList.AddRange(route.SavedHotels.Select(h => new SavedDto
            {
                Id = h.Id,
                RouteId = h.RouteId,
                Json = h.HotelJson,
                Type = "Hotel"
               
            }));

            savedList.AddRange(route.SavedFlights.Select(f => new SavedDto
            {
                Id= f.Id,
                RouteId = f.RouteId,
                Json = f.FlightsJson,
                Type = "Flight"
            }));

            savedList.AddRange(route.SavedPlaces.Select(f => new SavedDto
            {
                Id = f.Id,
                RouteId = f.RouteId,
                Json = f.PlaceJson,
                Type = "Place"
            }));



            return new ShowRouteDto
            {
                SavedJson = savedList
            };
        }
    }
}