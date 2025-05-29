using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Routes;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure.Services.Routes
{
    public class RouteService : IRouteService
    {
        private readonly YourTripsDbContext _context;
        private readonly IMapper _mapper;

        public RouteService(YourTripsDbContext context, IMapper mapper)
        {
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

        public async Task<ResultDto<RouteDetailsDto>> GetRouteDetailsAsync(int routeId)
        {
            var route = await _context.Routes
                .Include(r => r.SavedHotels)
                .Include(r => r.SavedFlights)
                .Include(r => r.SavedPlaces)
                .FirstOrDefaultAsync(r => r.Id == routeId);

            if (route == null)
                return ResultDto<RouteDetailsDto>.Fail("Маршрут не знайдено");

            var result = _mapper.Map<RouteDetailsDto>(route);

            // Додаткове наповнення SavedItems
            result.SavedItems = new List<SavedItemDto>();

            if (route.SavedHotels?.Any() == true)
                result.SavedItems.AddRange(route.SavedHotels.Select(h => new SavedItemDto
                {
                    Id = h.Id,
                    Type = "hotel",
                    SavedAt = h.SavedAt
                }));

            // Аналогічно для інших типів...

            return ResultDto<RouteDetailsDto>.Success(result);
        }
    }
}